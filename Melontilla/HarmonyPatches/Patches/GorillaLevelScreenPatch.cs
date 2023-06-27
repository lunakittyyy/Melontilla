using HarmonyLib;
using MelonLoader;
using UnityEngine;
using UnityEngine.UI;

namespace Melontilla.HarmonyPatches
{
    [HarmonyPatch(typeof(GorillaLevelScreen))]
    [HarmonyPatch("UpdateText")] // Just a guess
    internal class GorillaLevelScreenPatch
    {
        private static bool Prefix(GorillaLevelScreen __instance, ref Text ___myText)
        {
            Melon<MelontillaMod>.Logger.Msg("level screen patch prefix running");
            if (___myText == null || __instance.GetComponent<MeshRenderer>() == null)
            {
                return false;
            }
            return true;
        }
    }
}
