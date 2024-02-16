﻿using System.Collections.Generic;
using Photon.Pun;
using Melontilla.Utils;
using GorillaNetworking;
using ExitGames.Client.Photon;

namespace Melontilla
{
    public class MelontillaNetworkController : MonoBehaviourPunCallbacks
    {
        public static Events events;

        Events.RoomJoinedArgs lastRoom;

        public GamemodeManager gameModeManager;

        public override void OnJoinedRoom()
        {
            // trigger events
            bool isPrivate = false;
            string gamemode = "";
            if (PhotonNetwork.CurrentRoom != null)
            {
                var currentRoom = PhotonNetwork.NetworkingClient.CurrentRoom;
                isPrivate = !currentRoom.IsVisible ||
                            currentRoom.CustomProperties.ContainsKey("Description"); // Room Browser rooms
                if (currentRoom.CustomProperties.TryGetValue("gameMode", out var gamemodeObject))
                {
                    gamemode = gamemodeObject as string;
                }
            }

            // TODO: Generate dynamically
            var prefix = "ERROR";
            if (gamemode.Contains(Models.Gamemode.GamemodePrefix))
            {
                prefix = "CUSTOM";
            }
            else
            {
                var dict = new Dictionary<string, string> {
                    { "INFECTION", "INFECTION" },
                    { "CASUAL", "CASUAL"},
                    { "HUNT", "HUNT" },
                    { "BATTLE", "PAINTBRAWL"},
                };

                foreach (var item in dict)
                {
                    if (gamemode.Contains(item.Key))
                    {
                        prefix = item.Value;
                        break;
                    }
                }
            }
            GorillaComputer.instance.currentGameModeText.Value = "CURRENT MODE\n" + prefix;

            Events.RoomJoinedArgs args = new Events.RoomJoinedArgs
            {
                isPrivate = isPrivate,
                Gamemode = gamemode
            };
            events.TriggerRoomJoin(args);

            lastRoom = args;

            RoomUtils.ResetQueue();
        }

        public override void OnLeftRoom()
        {
            if (lastRoom != null)
            {
                events.TriggerRoomLeft(lastRoom);
                lastRoom = null;
            }

            GorillaComputer.instance.currentGameModeText.Value = "CURRENT MODE\n-NOT IN ROOM-";
        }

        public override void OnRoomPropertiesUpdate(Hashtable propertiesThatChanged)
        {
            if (!propertiesThatChanged.TryGetValue("gameMode", out var gameModeObject)) return;
            if (!(gameModeObject is string gameMode)) return;

            if (lastRoom.Gamemode.Contains(Models.Gamemode.GamemodePrefix) && !gameMode.Contains(Models.Gamemode.GamemodePrefix))
            {
                gameModeManager.OnRoomLeft(null, lastRoom);
            }

            lastRoom.Gamemode = gameMode;
            lastRoom.isPrivate = PhotonNetwork.CurrentRoom.IsVisible;

        }
    }
}