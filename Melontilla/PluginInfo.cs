using System;
using System.Linq;
using MelonLoader;
using Melontilla.Models;
using static Cinemachine.CinemachineTriggerAction.ActionSettings;

namespace Melontilla
{
    public class PluginInfo
    {
        public MelonMod Plugin { get; set; }
        public Gamemode[] Gamemodes { get; set; }
        public Action<string> OnGamemodeJoin { get; set; }
        public Action<string> OnGamemodeLeave { get; set; }

        public override string ToString()
        {
            return $"{Plugin.Info.Name} [{string.Join(", ", Gamemodes.Select(x => x.DisplayName))}]";
        }
    }
}
