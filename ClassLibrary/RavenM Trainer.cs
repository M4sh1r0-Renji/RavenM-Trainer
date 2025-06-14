using BepInEx;
using HarmonyLib;
using UnityEngine;

[BepInPlugin("com.yourname.ravenfield.trainer", "Ravenfield Trainer", "0.1.0")]
public class RavenfieldTrainer : BaseUnityPlugin
{
    private Harmony _harmony;

    void Awake()
    {
        _harmony = new Harmony("com.yourname.ravenfield.trainer");
        _harmony.Patch(
            original: AccessTools.Method("Actor:SetHealth"), 
            prefix: new HarmonyMethod(typeof(RavenfieldTrainer), nameof(Patch_SetHealth_Prefix))
        );
    }


    public static bool Patch_SetHealth_Prefix(object __instance, ref float health)
    {
        return false;
    }

    void OnDestroy()
    {
        _harmony.UnpatchSelf();
    }
}
