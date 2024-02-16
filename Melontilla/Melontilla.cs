using MelonLoader;
using Melontilla.HarmonyPatches;
using Melontilla.Utils;
using UnityEngine;

namespace Melontilla
{
    public class MelontillaMod : MelonMod
    {
        static Events events = null;
        public static GameObject MelonTillaHome;

        public override void OnLateInitializeMelon()
        {
            LoggerInstance.Warning("Melontilla is beta software. While considered somewhat stable, issues can still occur. Exercise caution, and good luck!");
            RoomUtils.RoomCode = RoomUtils.RandomString(6); // Generate a random room code in case we need it

            GameObject dataObject = new GameObject();
            MelonTillaHome = new GameObject("Melontilla");
            UnityEngine.Object.DontDestroyOnLoad(dataObject);
            UnityEngine.Object.DontDestroyOnLoad(MelonTillaHome);
            MelonTillaHome.AddComponent<MelontillaNetworkController>();

            events = new Events();

            MelontillaNetworkController.events = events;
            PostInitializedPatch.events = events;

            // MelonLoader automatically applies all patches
            // so calling a patching method is unneeded

            var go = new GameObject("CustomGamemodesManager");
            var gmm = go.AddComponent<GamemodeManager>();
            MelonTillaHome.GetComponent<MelontillaNetworkController>().gameModeManager = gmm;
        }
    }
}
