using HarmonyLib;
using MelonLoader;
using Photon.Pun;
using UnityEngine;

namespace Melontilla.HarmonyPatches
{
    [HarmonyPatch(typeof(PhotonNetwork))]
    [HarmonyPatch("CreateRoom", MethodType.Normal)]
    internal class PhotonNetworkPatch
    {
        public static bool setCasualPrivate = false;

        private static void Prefix(ref Photon.Realtime.RoomOptions roomOptions)
        {
            Melon<MelontillaMod>.Logger.Msg("photon prefix running");
            if (setCasualPrivate)
            {
                if (roomOptions.CustomRoomProperties["gameMode"] as string == "private")
                {
                    roomOptions.CustomRoomProperties["gameMode"] = "privateCASUAL";
                }

                setCasualPrivate = false;
            }
        }
    }
}

