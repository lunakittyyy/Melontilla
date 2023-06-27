using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using GorillaNetworking;
using HarmonyLib;
using Photon.Pun;
using UnityEngine;

namespace Melontilla.HarmonyPatches
{
    [HarmonyPatch(typeof(PhotonNetworkController))]
    [HarmonyPatch("ProcessJoiningPublicRoomState", MethodType.Normal)]
    internal class MatchmakingRegionPatch
    {
        static bool firstRegion = true;
        static List<int> usedRegions = new List<int>();

        private static bool Prefix(PhotonNetworkController __instance, PhotonNetworkController.ConnectionEvent connectionEvent, ref bool ___joiningWithFriend, ref bool ___pastFirstConnection, ref int[] ___playersInRegion)
        {
            switch (connectionEvent)
            {
                // Attempting to join a room, try and join a random room ignoring used regions
                case PhotonNetworkController.ConnectionEvent.OnDisconnected:
                    // These code paths do different things, don't alter them
                    if (___joiningWithFriend || !___pastFirstConnection) return true;

                    Debug.Log("attempt to join master and join a random room");

                    // Ignore used reigons
                    int[] playersInRegion = (int[])___playersInRegion.Clone();
                    foreach (int i in usedRegions)
                    {
                        playersInRegion[i] = 0;
                    }

                    // Lemming's code, choose a random region weighted with the players in each region
                    float value = UnityEngine.Random.value;
                    int num = 0;
                    for (int i = 0; i < playersInRegion.Length; i++)
                    {
                        num += playersInRegion[i];
                    }
                    float num2 = 0f;
                    int num3 = -1;
                    while (num2 < value && num3 < playersInRegion.Length - 1)
                    {
                        num3++;
                        num2 += (float)playersInRegion[num3] / (float)num;
                    }
                    PhotonNetwork.PhotonServerSettings.AppSettings.FixedRegion = __instance.serverRegions[num3];
                    Debug.Log("joining past the first room, so we weighted randomly picked " + __instance.serverRegions[num3]);
                    // End of lemming's code

                    // For some reason the first time this is called it just runs again, so don't ignore the region we just chose
                    if (!firstRegion)
                    {
                        usedRegions.Add(num3);
                    }
                    firstRegion = false;

                    PhotonNetwork.ConnectUsingSettings();

                    return false;

                // Failed to join a room in the region, try the next one 
                case PhotonNetworkController.ConnectionEvent.OnJoinRandomFailed:
                    if (usedRegions.Count < __instance.serverRegions.Length)
                    {
                        // Not enough server regions used, so we will try again
                        PhotonNetwork.Disconnect();
                        return false;
                    }
                    else
                    {
                        // Tried all of the regions, make a new room
                        return true;
                    }

                // Successsfully joined room, so reset everything
                case PhotonNetworkController.ConnectionEvent.OnJoinedRoom:
                    usedRegions = new List<int>();
                    firstRegion = true;
                    return true;

                // Not a case we care about, return
                default:
                    return true;
            }
        }
    }
}