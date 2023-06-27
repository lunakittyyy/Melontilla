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
        GameObject MelonTillaHome;

        public override void OnInitializeMelon()
        {
            RoomUtils.RoomCode = RoomUtils.RandomString(6); // Generate a random room code in case we need it

            GameObject MelonTillaHome = new GameObject();
            GameObject dataObject = new GameObject();
            UnityEngine.Object.DontDestroyOnLoad(dataObject);
            MelonTillaHome.AddComponent<UtillaNetworkController>();

            Events.GameInitialized += PostInitialized;

            UtillaNetworkController.events = events;
            PostInitializedPatch.events = events;

            // MelonLoader automatically applies all patches
            // so calling a patching method is unneeded
            // UtillaPatches.ApplyHarmonyPatches();
        }

        void PostInitialized(object sender, EventArgs e)
        {
            var go = new GameObject("CustomGamemodesManager");
            var gmm = go.AddComponent<GamemodeManager>();
            MelonTillaHome.GetComponent<UtillaNetworkController>().gameModeManager = gmm;
        }
    }
}
