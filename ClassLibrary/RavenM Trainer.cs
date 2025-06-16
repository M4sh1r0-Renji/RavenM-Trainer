using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.InteropServices;
using HarmonyLib;
using BepInEx;
using BepInEx.Logging;
using UnityEngine;
using UnityEngine.UI;
using Ravenfield.SpecOps;
using Ravenfield.Trigger;

namespace RavenM.Trainer
{
    [BepInPlugin("com.example.ravenmtrainer", "RavenM Trainer", "0.1.0")]
    public class TrainerPlugin : BaseUnityPlugin
    {
        // Logger instance (hide base Logger)
        private static new ManualLogSource Logger;
        
        // Toggle for GUI display
        private bool _showGui = false;

        private void Awake()
        {
            // Initialize logger
            Logger = base.Logger;
            Logger.LogInfo("RavenM Trainer loaded");
            
            // Apply Harmony patches
            Harmony.CreateAndPatchAll(Assembly.GetExecutingAssembly(), "com.example.ravenmtrainer");
        }

        private void Update()
        {
            // Toggle GUI with F6
            if (Input.GetKeyDown(KeyCode.F6))
            {
                _showGui = !_showGui;
                // Update cursor lock state
                Cursor.visible = _showGui;
                Cursor.lockState = _showGui ? CursorLockMode.None : CursorLockMode.Locked;
            }
        }

        private void OnGUI()
        {
            if (!_showGui) return;

            UnityEngine.GUILayout.BeginArea(new Rect(10, 10, 300, Screen.height - 20));
            UnityEngine.GUILayout.Label("=== RavenM Trainer ===");

            if (UnityEngine.GUILayout.Button("Toggle Infinite Ammo"))
            {
                // TODO: implement infinite ammo toggle
                Logger.LogInfo("Infinite Ammo toggled");
            }

            if (UnityEngine.GUILayout.Button("Toggle God Mode"))
            {
                // TODO: implement god mode toggle
                Logger.LogInfo("God Mode toggled");
            }

            UnityEngine.GUILayout.EndArea();
        }
    }
}
