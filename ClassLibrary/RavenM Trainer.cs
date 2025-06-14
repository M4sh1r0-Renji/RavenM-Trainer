using System;
using BepInEx;
using BepInEx.Logging;
using HarmonyLib;
using UnityEngine;


namespace RavenfieldTrainer
{
    [BepInPlugin("com.yourname.ravenfield.trainer", "Ravenfield Trainer", "0.1.0")]
    public class RavenfieldTrainer : BaseUnityPlugin
    {
        private Harmony _harmony;

        private void Awake()
        {
            _harmony = new Harmony("com.yourname.ravenfield.trainer");
            _harmony.PatchAll();

            Logger.LogInfo("Ravenfield Trainer loaded, Harmony patches applied.");
        }

        private void OnDestroy()
        {
            _harmony.UnpatchSelf();
            Logger.LogInfo("Ravenfield Trainer unloaded, Harmony patches removed.");
        }
    }
}

