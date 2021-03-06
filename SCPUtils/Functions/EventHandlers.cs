using Exiled.API.Features;
using Exiled.Events.EventArgs;
using MEC;
using System;
using Log = Exiled.API.Features.Log;
using Round = Exiled.API.Features.Round;
using System.Collections.Generic;
using System.Linq;

namespace SCPUtils
{
    public class EventHandlers
    {
        private readonly ScpUtils pluginInstance;

        public DateTime lastTeslaEvent;

        public static bool TemporarilyDisabledWarns;

        public EventHandlers(ScpUtils pluginInstance) => this.pluginInstance = pluginInstance;


        internal void OnPlayerDeath(DyingEventArgs ev)
        {
            if ((ev.Target.Team == Team.SCP || (pluginInstance.Config.AreTutorialsSCP && ev.Target.Team == Team.TUT)) && Round.IsStarted && pluginInstance.Config.EnableSCPSuicideAutoWarn && !TemporarilyDisabledWarns)
            {
                if ((DateTime.Now - lastTeslaEvent).Seconds >= pluginInstance.Config.Scp079TeslaEventWait)
                {
                    if (ev.HitInformation.GetDamageType() == DamageTypes.Tesla || (ev.HitInformation.GetDamageType() == DamageTypes.Wall && ev.HitInformation.Amount >= 50000) || (ev.HitInformation.GetDamageType() == DamageTypes.Grenade && ev.Killer == ev.Target)) pluginInstance.Functions.OnQuitOrSuicide(ev.Target);
                    else if ((ev.HitInformation.GetDamageType() == DamageTypes.Wall && ev.HitInformation.Amount == -1f) && ev.Killer == ev.Target && pluginInstance.Config.QuitEqualsSuicide) pluginInstance.Functions.OnQuitOrSuicide(ev.Target);
                }
            }
        }

        internal void OnRoundEnded(RoundEndedEventArgs _)
        {
            foreach (var player in Exiled.API.Features.Player.List)
            {
                pluginInstance.Functions.SaveData(player);
            }
            TemporarilyDisabledWarns = true;
        }

        internal void OnPlayerDestroy(DestroyingEventArgs ev)
        {
            pluginInstance.Functions.SaveData(ev.Player);
        }

        internal void OnWaitingForPlayers()
        {
            TemporarilyDisabledWarns = false;
        }

        internal void On079TeslaEvent(InteractingTeslaEventArgs _) => lastTeslaEvent = DateTime.Now;


        internal void OnPlayerHurt(HurtingEventArgs ev)
        {
            if (pluginInstance.Config.CuffedImmunityPlayers?.ContainsKey(ev.Target.Team) == true)
            {
                ev.IsAllowed = !(pluginInstance.Functions.IsTeamImmune(ev.Target, ev.Attacker) && pluginInstance.Functions.CuffedCheck(ev.Target) && pluginInstance.Functions.CheckSafeZones(ev.Target));
            }
        }

  
        internal void OnPlayerVerify(VerifiedEventArgs ev)
        {                    
           
            if (!Database.LiteDatabase.GetCollection<Player>().Exists(player => player.Id == DatabasePlayer.GetRawUserId(ev.Player)))
            {
                Log.Info(ev.Player.Nickname + " is not present on DB, creating account!");
                pluginInstance.DatabasePlayerData.AddPlayer(ev.Player);
            }

            var databasePlayer = ev.Player.GetDatabasePlayer();
            if (Database.PlayerData.ContainsKey(ev.Player)) return;
            Database.PlayerData.Add(ev.Player, databasePlayer);
            databasePlayer.LastSeen = DateTime.Now;
            databasePlayer.Name = ev.Player.Nickname;
            if (databasePlayer.FirstJoin == DateTime.MinValue) databasePlayer.FirstJoin = DateTime.Now;
            if (pluginInstance.Config.WelcomeEnabled) ev.Player.Broadcast(pluginInstance.Config.WelcomeMessageDuration, pluginInstance.Config.WelcomeMessage, Broadcast.BroadcastFlags.Normal);
            if (pluginInstance.Functions.CheckAsnPlayer(ev.Player)) ev.Player.Kick($"Auto-Kick: {pluginInstance.Config.AsnKickMessage}", "SCPUtils");
            else pluginInstance.Functions.PostLoadPlayer(ev.Player);
        }

        internal void OnPlayerSpawn(SpawningEventArgs ev)
        {
            if (ev.Player.Team == Team.SCP || (pluginInstance.Config.AreTutorialsSCP && ev.Player.Team == Team.TUT)) ev.Player.GetDatabasePlayer().TotalScpGamesPlayed++;
        }

        internal void OnPlayerLeave(LeftEventArgs ev)
        {
            pluginInstance.Functions.SaveData(ev.Player);
        }

        internal void OnDecontaminate(DecontaminatingEventArgs ev)
        {
            if (pluginInstance.Config.DecontaminationMessageEnabled) Map.Broadcast(pluginInstance.Config.DecontaminationMessageDuration, pluginInstance.Config.DecontaminationMessage, Broadcast.BroadcastFlags.Normal);
        }

    }

}
