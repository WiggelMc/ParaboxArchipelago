using System;
using System.IO;
using HarmonyLib;
using ParaboxArchipelago.State;

namespace ParaboxArchipelago.Patches
{
    public static class SaveFilePatches
    {
        [HarmonyPatch(typeof(SaveFile), "GetSaveFilePath")]
        public static class SaveFile_GetSaveFilePath
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
        public static class Prefs_GetPrefsFilePath
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
        
        [HarmonyPatch(typeof(TextWriter), nameof(TextWriter.WriteLine), typeof(string))]
        public static class TextWriter_WriteLine
        {
            public static void Prefix(TextWriter __instance, string value)
            {
                if (!ParaboxArchipelagoPlugin.MethodState.IsMethodRunning(nameof(Prefs_Save))) return;
                if (value != "#") return;
                
                var prefState = ParaboxArchipelagoPlugin.PrefState;

                WritePref(__instance, PrefState.ENABLE_ITEM_TRACKER_KEY, prefState.EnableItemTracker.ToString());
                WritePref(__instance, PrefState.ENABLE_LOCATION_TRACKER_KEY, prefState.EnableLocationTracker.ToString());
                WritePref(__instance, PrefState.ENABLE_ITEM_TRACKER_OVERLAY_KEY, prefState.EnableItemTrackerOverlay.ToString());
                WritePref(__instance, PrefState.ENABLE_LOCATION_TRACKER_OVERLAY_KEY, prefState.EnableLocationTrackerOverlay.ToString());
            }

            private static void WritePref(TextWriter writer, string key, string value)
            {
                writer.WriteLine(key + " " + value);
            }
        }
        
        [HarmonyPatch(typeof(StreamReader), nameof(StreamReader.ReadLine), new Type[]{})]
        public static class StreamReader_ReadLine
        {
            public static void Postfix(string __result)
            {
                if (!ParaboxArchipelagoPlugin.MethodState.IsMethodRunning(nameof(Prefs_Load))) return;
                if (__result == null) return;
                
                var strArray = __result.Split(' ');
                var prefState = ParaboxArchipelagoPlugin.PrefState;
                switch (strArray[0])
                {
                    case PrefState.ENABLE_ITEM_TRACKER_KEY:
                        prefState.EnableItemTracker = bool.Parse(strArray[1]);
                        break;
                    case PrefState.ENABLE_LOCATION_TRACKER_KEY:
                        prefState.EnableLocationTracker = bool.Parse(strArray[1]);
                        break;
                    case PrefState.ENABLE_ITEM_TRACKER_OVERLAY_KEY:
                        prefState.EnableItemTrackerOverlay = bool.Parse(strArray[1]);
                        break;
                    case PrefState.ENABLE_LOCATION_TRACKER_OVERLAY_KEY:
                        prefState.EnableLocationTrackerOverlay = bool.Parse(strArray[1]);
                        break;
                }
            }
        }

        [HarmonyPatch(typeof(Prefs), nameof(Prefs.DefaultPrefs))]
        public static class Prefs_DefaultPrefs
        {
            public static void Postfix()
            {
                var prefState = ParaboxArchipelagoPlugin.PrefState;
                prefState.EnableItemTracker = false;
                prefState.EnableLocationTracker = false;
                prefState.EnableItemTrackerOverlay = false;
                prefState.EnableItemTrackerOverlay = false;
                ParaboxArchipelagoPlugin.Log.LogInfo("LOAD PREF DEFAULT");
            }
        }
        
        [HarmonyPatch(typeof(Prefs), nameof(Prefs.Load))]
        public static class Prefs_Load
        {
            [HarmonyPriority(-100_000)]
            public static void Prefix()
            {
                ParaboxArchipelagoPlugin.MethodState.StartMethod(nameof(Prefs_Load));
            }

            [HarmonyPriority(100_000)]
            public static void Postfix()
            {
                ParaboxArchipelagoPlugin.MethodState.EndMethod(nameof(Prefs_Load));
            }
        }
        
        [HarmonyPatch(typeof(Prefs), nameof(Prefs.Save))]
        public static class Prefs_Save
        {
            [HarmonyPriority(-100_000)]
            public static void Prefix()
            {
                ParaboxArchipelagoPlugin.MethodState.StartMethod(nameof(Prefs_Save));
            }

            [HarmonyPriority(100_000)]
            public static void Postfix()
            {
                ParaboxArchipelagoPlugin.MethodState.EndMethod(nameof(Prefs_Save));
            }
        }
    }
}