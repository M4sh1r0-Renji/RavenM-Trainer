using System.IO;
using BepInEx;
using BepInEx.Logging;
using HarmonyLib;
using UnityEngine;
using UnityEngine.Rendering;

namespace RavenMTrainer
{
    [BepInPlugin("com.yourname.ravenfield.trainer", "Ravenfield Trainer", "0.1.0")]
    public class RavenfieldTrainer : BaseUnityPlugin
    {
        internal static new ManualLogSource Logger;

        private Harmony _harmony;

        private bool _showGui = false;

        private bool _infiniteAmmo = false;
        private bool _infiniteHealth = false;
        private bool _oneHitKill = false;
        private bool _wallhack = false;

        void Awake()
        {
            Logger = base.Logger;
            Logger.LogInfo("Ravenfield Trainer loaded");

            _harmony = new Harmony("com.yourname.ravenfield.trainer");
            _harmony.PatchAll();

            Shader.WarmupAllShaders();
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Period))
                _showGui = !_showGui;
            if (_infiniteAmmo)    ApplyInfiniteAmmo();
            if (_infiniteHealth)  ApplyInfiniteHealth();
            if (_oneHitKill)      ApplyOneHitKill();
            if (_wallhack)        ApplyWallhack();
        }

        void OnGUI()
        {
            if (!_showGui) return;

            GUILayout.BeginArea(new Rect(10, 10, 200, 200), "Trainer", GUI.skin.window);
            _infiniteAmmo   = GUILayout.Toggle(_infiniteAmmo,   "Infinite Ammo");
            _infiniteHealth = GUILayout.Toggle(_infiniteHealth, "Infinite Health");
            _oneHitKill     = GUILayout.Toggle(_oneHitKill,     "One Hit Kill");
            _wallhack       = GUILayout.Toggle(_wallhack,       "Wallhack");
            GUILayout.EndArea();
        }

        void OnDestroy()
        {
            _harmony.UnpatchSelf();
        }

        #region
        private void ApplyInfiniteAmmo()
        {
            var player = PlayerManager.instance.player; // PSEUDOCODE
            if (player?.currentWeapon != null)
                player.currentWeapon.ammo = player.currentWeapon.maxAmmo;
        }

        private void ApplyInfiniteHealth()
        {
            var player = PlayerManager.instance.player;
            if (player != null)
                player.SetHealth(player.maxHealth);
        }

        private void ApplyOneHitKill()
        {

        }

        private void ApplyWallhack()
        {
            foreach (var actor in FindObjectsOfType<Actor>())
            {
                var rend = actor.GetComponentInChildren<Renderer>();
                if (rend != null)
                {
                    var mat = rend.material;
                    mat.SetInt("_ZWrite",   0);
                    mat.SetInt("_ZTest",    (int)CompareFunction.Always);
                    mat.renderQueue = 5000;
                }
            }
        }
        #endregion
    }

    [HarmonyPatch(typeof(Actor), "TakeDamage")]
    class Patch_OneHitKill
    {
        static void Prefix(ref float damage)
        {
            if (/* yourTrainerInstance._oneHitKill */ false)
                damage = 9999f;
        }
    }
}
