﻿using Exiled.API.Interfaces;
using System.Collections.Generic;
using System.ComponentModel;
using Log = Exiled.API.Features.Log;
using ZoneType = Exiled.API.Enums.ZoneType;


namespace SCPUtils
{
    public class Configs : IConfig
    {
        public bool IsEnabled { get; set; } = true;

        [Description("Should SCPs be warned for quitting or suicide?")]
        public bool EnableSCPSuicideAutoWarn { get; private set; } = true;

        [Description("Should round autorestart if there is only one player left?")]
        public bool EnableRoundRestartCheck { get; private set; } = true;

        [Description("Are quits as SCP considered a warnable offence?")]
        public bool QuitEqualsSuicide { get; private set; } = true;

        [Description("Should welcome message be shown?")]
        public bool WelcomeEnabled { get; private set; } = true;

        [Description("Should decontamination message be shown?")]
        public bool DecontaminationMessageEnabled { get; private set; } = false;

        [Description("Should SCPs be kicked for quitting or suicide after a certain threshold?")]
        public bool AutoKickOnSCPSuicide { get; private set; } = true;

        [Description("Should SCPs be banned for quitting or suicide after a certain threshold?")]
        public bool EnableSCPSuicideAutoBan { get; private set; } = true;

        [Description("Should ban duration multiply after each ban for quit / suicide as SCP?")]
        public bool MultiplyBanDurationEachBan { get; private set; } = true;

        [Description("Should player preferences be reset after badge expire?")]
        public bool ResetPreferencedOnBadgeExpire { get; private set; } = true;

        [Description("Should a player with a banned game be autokicked?")]
        public bool AutoKickBannedNames { get; private set; } = true;

        [Description("Should Tutorials be considered as SCP? If true they will get warned for suicides / quits if enabled")]
        public bool AreTutorialsSCP { get; private set; } = false;

        [Description("Broadcast in admin chat auto kick and bans for quitting or suicide as SCP")]
        public bool BroadcastSanctions { get; private set; } = true;

        [Description("Broadcast in admin chat auto warns for quitting or suicide as SCP")]
        public bool BroadcastWarns { get; private set; } = false;

        [Description("Should the nickname gets resetted if the user when joins doesn't have the permission to execute the command? You can bypass it using scputils_preference_persist command")]
        public bool KeepNameWithoutPermission { get; private set; } = false;

        [Description("Should the color gets resetted if the user when joins doesn't have the permission to execute the command? You can bypass it using scputils_preference_persist command")]
        public bool KeepColorWithoutPermission { get; private set; } = false;

        [Description("Should the badge visibility gets resetted if the user when joins doesn't have the permission to execute the command? You can bypass it using scputils_preference_persist command")]
        public bool KeepBadgeVisibilityWithoutPermission { get; private set; } = false;

        [Description("Autowarn message for suiciding as SCP")]
        public string SuicideWarnMessage { get; private set; } = "<color=red>WARN:\nAs per server rules SCP's suicide is an offence, doing it too much will result in a ban!</color>";

        [Description("Welcome message (if enabled)")]
        public string WelcomeMessage { get; private set; } = "<color=green>Welcome to the server!</color>";

        [Description("Decontamination message (if enabled)")]
        public string DecontaminationMessage { get; private set; } = "<color=yellow>Decontamination has started</color>";

        [Description("Suicide auto-kick reason (if enabled)")]
        public string SuicideKickMessage { get; private set; } = "Suicide as SCP";

        [Description("Auto-kick message for using a restricted nickname")]
        public string AutoKickBannedNameMessage { get; private set; } = "You're using a restricted nickname or too similar to a restricted one, please change it";

        [Description("Suicide auto-ban reason (if enabled)")]
        public string AutoBanMessage { get; private set; } = "Exceeded SCP suicide limit";

        [Description("Message if player is not authorized to use this command")]
        public string UnauthorizedNickNameChange { get; private set; } = "<color=red>Permission denied.</color>";

        [Description("Message if player is not authorized to use this command")]
        public string UnauthorizedColorChange { get; private set; } = "<color=red>Permission denied.</color>";

        [Description("Message if player is not authorized to use this command")]
        public string UnauthorizedBadgeChangeVisibility { get; private set; } = "<color=red>Permission denied.</color>";

        [Description("Message if player try to change his nickname to a restricted one")]
        public string InvalidNicknameText { get; private set; } = "This nickname has been restricted by server owner, please use another nickname!";

        [Description("Database name, change it only if you are running multiple servers")]
        public string DatabaseName { get; private set; } = "SCPUtils";

        [Description("In which folder database should be stored?")]
        public string DatabaseFolder { get; private set; } = "EXILED";

        [Description("Welcome message duration (if enabled)")]
        public ushort WelcomeMessageDuration { get; private set; } = 12;

        [Description("Decontamination message duration (if enabled)")]
        public ushort DecontaminationMessageDuration { get; private set; } = 10;

        [Description("Auto-restart time if there is only one player in server (if enabled)")]
        public ushort AutoRestartTime { get; private set; } = 15;

        [Description("Auto-warn message duration (if enabled)")]
        public ushort AutoWarnMessageDuration { get; private set; } = 30;

        [Description("Which is the minimun number of suicides before the player may not receive any kick o ban ignoring the SCP suicides / quit percentage? (if enabled)")]
        public int ScpSuicideTollerance { get; private set; } = 5;

        [Description("SCP Suicide / Quit base ban duration (if enabled)")]
        public int AutoBanDuration { get; private set; } = 15;

        [Description("Which is the max length of a nickname using change name command?")]
        public int NicknameMaxLength { get; private set; } = 32;

        [Description("If 079 trigger tesla for how many seconds player shouldn't get warned for suicide? (2 is enough for most of servers)")]
        public int Scp079TeslaEventWait { get; private set; } = 2;

        [Description("Which quit / suicide percentage as SCP a player require before getting banned? (You can add tollerence in settings)")]
        public float AutoBanThreshold { get; private set; } = 30.5f;

        [Description("Which quit / suicide percentage as SCP a player require before getting kicked? (You can add tollerence in settings)")]
        public float AutoKickThreshold { get; private set; } = 15.5f;

        [Description("Which colors are restricted on .scputils_change_color command?")]
        public List<string> RestrictedRoleColors { get; private set; } = new List<string>() { "Color1", "Color2" };

        [Description("Which nicknames are restricted on .scputils_change_nickname command?")]
        public List<string> BannedNickNames { get; private set; } = new List<string>() { "@everyone", "@here", "Admin" };

        [Description("Which ASNs should be blacklisted? Players to connect from blacklisted ASN should be whitelisted via scputils_whitelist_asn command (50889 is geforce now ASN)")]
        public List<string> ASNBlacklist { get; private set; } = new List<string>() { "50889" };


        [Description("Which message non-whitelisted players should get while connecting from blacklisted ASN?")]
        public string AsnKickMessage { get; private set; } = "The ASN you are connecting from is blacklisted from this server, please contact server staff to request to being whitelisted";


        /*

          public Dictionary<Team, ImmunityPlayers> ImmunityPlayers { get; private set; } = new Dictionary<Team, ImmunityPlayers>()
          {
              //Class-D example dictionary config
              {
                Team.CDP, //key
                new ImmunityPlayers
                {                  
                   Attacker = new List<Team>() { Team.RSC, Team.MTF }, //Attacker List
                   ShouldBeCuffed = true, //Cuffed
                   ImmunityZones = new List<ZoneType>() { ZoneType.Entrance, ZoneType.Surface } //Zone List                  
                }
              },

              //Rip example dictionary config
              {
                Team.RIP, //key
                new ImmunityPlayers
                {
                    Attacker = new List<Team>() { Team.TUT }, //Attacker List
                    ShouldBeCuffed = false, //Cuffed
                    ImmunityZones = new List<ZoneType>() { ZoneType.Entrance, ZoneType.Surface, ZoneType.LightContainment, ZoneType.HeavyContainment, ZoneType.Unspecified } //Zone list
                }
              }
          };

          */

        [Description("You have to add the team you want to protect from the target as key and enemy teams on the list as value, on github documentation you can see all the teams.")]

        public Dictionary<Team, List<Team>> CuffedImmunityPlayers { get; private set; } = new Dictionary<Team, List<Team>>();


        [Description("Indicates if the protected teams should be cuffed to get the protection, if you don't add a team it will get protection regardless")]

        public List<Team> CuffedProtectedTeams { get; private set; } = new List<Team>();

        [Description("Indicates in which zones the protected team is protected, Zone list: Surface, Entrance, HeavyContainment, LightContainment, Unspecified")]

        public Dictionary<Team, List<ZoneType>> CuffedSafeZones { get; private set; } = new Dictionary<Team, List<ZoneType>>();

        [Description("With which SCPs users are allowed to speak using V to humans without any badge (remove the disallowed SCPs)? (This will bypass permissions check so everyone will be able to speak with those SCPs regardless rank)")]

        public List<RoleType> AllowedScps { get; private set; } = new List<RoleType>() { RoleType.Scp049, RoleType.Scp0492, RoleType.Scp079, RoleType.Scp096, RoleType.Scp106, RoleType.Scp173, RoleType.Scp93953, RoleType.Scp93989 };


        public void ConfigValidator()
        {
            if (ScpSuicideTollerance < 0)
            {
                Log.Warn("Invalid config scputils_scp_suicide_tollerance, loading dafault one!");
                ScpSuicideTollerance = 5;

            }
            if (AutoKickThreshold >= AutoBanThreshold)
            {
                Log.Warn("Invalid config scputils_auto_kick_threshold OR scputils_auto_ban_threshold, loading dafault one!");
                AutoBanThreshold = 30.5f;

            }
            if (AutoRestartTime < 0)
            {
                Log.Warn("Invalid config scputils_auto_restart_time, loading dafault one!");
                AutoRestartTime = 15;
            }
            if (Scp079TeslaEventWait < 0)
            {
                Log.Warn("Invalid config scputils_scp_079_tesla_event_wait, loading dafault one!");
                Scp079TeslaEventWait = 2;
            }
            if (!IsEnabled)
            {
                Log.Warn("You disabled the plugin in server configs!");
            }
        }
    }
}
