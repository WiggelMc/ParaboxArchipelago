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

                WritePref(__instance, PrefState.PERSONAL_ITEM_FEED_WINDOW_KEY, prefState.PersonalItemFeedWindow.ToString());
                WritePref(__instance, PrefState.OTHER_ITEM_FEED_WINDOW_KEY, prefState.OtherItemFeedWindow.ToString());
                WritePref(__instance, PrefState.CHAT_WINDOW_KEY, prefState.ChatWindow.ToString());
                WritePref(__instance, PrefState.ITEM_TRACKER_WINDOW_KEY, prefState.ItemTrackerWindow.ToString());
                WritePref(__instance, PrefState.LOCATION_TRACKER_WINDOW_KEY, prefState.LocationTrackerWindow.ToString());
                WritePref(__instance, PrefState.CONNECTION_WINDOW_KEY, prefState.ConnectionWindow.ToString());
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
                    case PrefState.PERSONAL_ITEM_FEED_WINDOW_KEY:
                        prefState.PersonalItemFeedWindow = WindowState.Parse(strArray[1]);
                        break;
                    case PrefState.OTHER_ITEM_FEED_WINDOW_KEY:
                        prefState.OtherItemFeedWindow = WindowState.Parse(strArray[1]);
                        break;
                    case PrefState.CHAT_WINDOW_KEY:
                        prefState.ChatWindow = WindowState.Parse(strArray[1]);
                        break;
                    case PrefState.ITEM_TRACKER_WINDOW_KEY:
                        prefState.ItemTrackerWindow = WindowState.Parse(strArray[1]);
                        break;
                    case PrefState.LOCATION_TRACKER_WINDOW_KEY:
                        prefState.LocationTrackerWindow = WindowState.Parse(strArray[1]);
                        break;
                    case PrefState.CONNECTION_WINDOW_KEY:
                        prefState.ConnectionWindow = WindowState.Parse(strArray[1]);
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
                
                prefState.PersonalItemFeedWindow = PrefState.PersonalItemFeedWindowDefault;
                prefState.OtherItemFeedWindow = PrefState.OtherItemFeedWindowDefault;
                prefState.ChatWindow = PrefState.ChatWindowDefault;
                prefState.ItemTrackerWindow = PrefState.ItemTrackerWindowDefault;
                prefState.LocationTrackerWindow = PrefState.LocationTrackerWindowDefault;
                prefState.ConnectionWindow = PrefState.ConnectionWindowDefault;

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