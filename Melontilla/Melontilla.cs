using System;
using MelonLoader;
using Melontilla.HarmonyPatches;
using Melontilla.Utils;
using UnityEngine;
using System.Linq;
using Photon.Realtime;
using Photon.Pun;
using System.Collections.Generic;

namespace Melontilla
{
    public class MelontillaMod : MelonMod
    {
        static Events events = new Events();
        public static GameObject MelonTillaHome;

        public override void OnLateInitializeMelon()
        {
            LoggerInstance.Warning("Melontilla is highly WIP and experimental. Melontilla is provided 'as is' without warranty of any kind. The authors are not liable for any nasty issues like bans that Melontilla may cause.");
            LoggerInstance.Msg("gening random roomcode");
            RoomUtils.RoomCode = RoomUtils.RandomString(6); // Generate a random room code in case we need it

            LoggerInstance.Msg("creating data object");
            GameObject dataObject = new GameObject();
            LoggerInstance.Msg("creating a home for melontilla to be happy in");
            MelonTillaHome = new GameObject("Melontilla");

            LoggerInstance.Msg("making dataobject dontdestroyonload");
            UnityEngine.Object.DontDestroyOnLoad(dataObject);
            LoggerInstance.Msg("making melontilla dontdestroyonload");
            UnityEngine.Object.DontDestroyOnLoad(MelonTillaHome);
            LoggerInstance.Msg("adding network controller");
            MelonTillaHome.AddComponent<UtillaNetworkController>();

            UtillaNetworkController.events = events;
            PostInitializedPatch.events = events;

            // MelonLoader automatically applies all patches
            // so calling a patching method is unneeded
            // UtillaPatches.ApplyHarmonyPatches();
            LoggerInstance.Msg("creating gamemode manager object");
            var go = new GameObject("CustomGamemodesManager");
            LoggerInstance.Msg("adding gamemode manager");
            var gmm = go.AddComponent<GamemodeManager>();
            LoggerInstance.Msg("setting melontilla's networkcontroller gamemode manager to the one we added");
            MelonTillaHome.GetComponent<UtillaNetworkController>().gameModeManager = gmm;
        }
    }
}
