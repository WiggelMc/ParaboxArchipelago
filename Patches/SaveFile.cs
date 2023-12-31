using BepInEx.Logging;
using HarmonyLib;

namespace ParaboxArchipelago.Patches
{
    public static class SaveFile
    {
        [HarmonyPatch(typeof(global::SaveFile), "GetSaveFilePath")]
        public class SaveFile_GetSaveFilePath
        {
            public static void Postfix(ref string __result, int slot)
            {
                var suffix = slot + ".txt";
                if (__result.EndsWith(suffix))
                {
                    __result = __result.Substring(0, __result.Length - suffix.Length) + "_ap_rando_SEEDGOESHERE.txt";
                }
                else
                {
                    ParaboxArchipelagoPlugin.Log.LogWarning("Save File could not be changed");
                }
            }
        }
    
        [HarmonyPatch(typeof(Prefs), nameof(Prefs.GetPrefsFilePath))]
        public class Prefs_GetPrefsFilePath
        {
            public static void Postfix(ref string __result)
            {
                const string suffix = ".txt";
                if (__result.EndsWith(suffix))
                {
                    __result = __result.Substring(0, __result.Length - suffix.Length) + "_ap_rando.txt";
                }
                else
                {
                    ParaboxArchipelagoPlugin.Log.LogWarning("Prefs File could not be changed");
                }
            }
        }
    }
}