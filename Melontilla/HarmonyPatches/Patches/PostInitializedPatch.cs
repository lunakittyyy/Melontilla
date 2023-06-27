using UnityEngine;
using HarmonyLib;
using System.Collections;
using System.Threading.Tasks;
using Melontilla;
using MelonLoader;

namespace Melontilla.HarmonyPatches
{
    [HarmonyPatch(typeof(GorillaTagger))]
    [HarmonyPatch("Start", MethodType.Normal)]
    static class PostInitializedPatch
    {
        public static Events events;

        private static void Postfix()
        {
            Melon<MelontillaMod>.Logger.Msg("init postfix running");
            // await Task.Yield();
            events.TriggerGameInitialized();
        }
    }
}
