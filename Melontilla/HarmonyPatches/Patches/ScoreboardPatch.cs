using HarmonyLib;
using MelonLoader;
using UnityEngine;

namespace Melontilla.HarmonyPatches
{
    [HarmonyPatch(typeof(GorillaScoreboardSpawner))]
    [HarmonyPatch("OnJoinedRoom")] // Just a guess
    internal class ScoreboardPatch
    {
        private static void Prefix(GorillaScoreboardSpawner __instance)
        {
            Melon<MelontillaMod>.Logger.Msg("scoreboard prefix running");
            if (__instance.notInRoomText == null)
            {
                __instance.notInRoomText = new GameObject();
            }
        }
    }
}
