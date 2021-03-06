﻿using System;
using CommandSystem;
using Exiled.Permissions.Extensions;

namespace SCPUtils.Commands
{
    [CommandHandler(typeof(RemoteAdminCommandHandler))]
    [CommandHandler(typeof(GameConsoleCommandHandler))]
    [CommandHandler(typeof(ClientCommandHandler))]
    class Help : ICommand
    {
        public string Command { get; } = "scputils_help";

        public string[] Aliases { get; } = new string[] { };

        public string Description { get; } = "Show plugin info";

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            string text = "";
            text = $"<color=#1BBC9B>User commands:</color> \n" +
               "<color=#1BBC9B>.scputils_info, .scputils_change_nickname, .scputils_change_color, .scputils_show_badge, .scputils_hide_badge, .scputils_my_info, .scputils_play_time</color>";
            if (sender.CheckPermission("scputils.help")) text += "\n<color=#FFD700>Administration commands (Remote Admin): </color>\n" +
                    "<color=#FFD700>scputils_player_info, scputils_player_list, scputils_player_reset_preferences, scputils_player_reset, scputils_set_color, scputils_set_name, scputils_set_badge, scputils_revoke_badge, scputils_play_time, scputils_whitelist_asn, scputils_unwhitelist_asn, scputils_enable_suicide_warns, scputils_disable_suicide_warns, scputils_global_edit, scputils_player_edit, scputils_player_delete, scputils_player_restrict, scputils_player_unrestrict, scputils_show_command_bans, scputils_preference_persist, scputils_remove_previous_badge</color>";
            response = text;
            return true;
        }
    }
}