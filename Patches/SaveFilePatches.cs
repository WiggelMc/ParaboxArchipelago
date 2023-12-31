using System;
using System.IO;
using HarmonyLib;

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
                if (ParaboxArchipelagoPlugin.State.IsMethodRunning(nameof(Prefs_Save)))
                {
                    if (value == "#")
                    {
                        __instance.WriteLine("ap_enable_item_tracker " + false);
                    }
                }
            }
        }
        
        [HarmonyPatch(typeof(StreamReader), nameof(StreamReader.ReadLine), new Type[]{})]
        public static class StreamReader_ReadLine
        {
            public static void Postfix(string __result)
            {
                if (ParaboxArchipelagoPlugin.State.IsMethodRunning(nameof(Prefs_Load)))
                {
                    if (__result != null)
                    { 
                        string[] strArray = __result.Split(' ');
                        switch (strArray[0]) 
                        {
                            case "ap_enable_item_tracker":
                                ParaboxArchipelagoPlugin.Log.LogInfo("LOAD PREF " + bool.Parse(strArray[1]));
                                break;
                        }
                    }
                }
            }
        }

        [HarmonyPatch(typeof(Prefs), nameof(Prefs.Load))]
        public static class Prefs_Load
        {
            [HarmonyPriority(-100_000)]
            public static void Prefix()
            {
                ParaboxArchipelagoPlugin.State.StartMethod(nameof(Prefs_Load));
            }

            [HarmonyPriority(100_000)]
            public static void Postfix()
            {
                ParaboxArchipelagoPlugin.State.EndMethod(nameof(Prefs_Load));
            }
        }
        
        [HarmonyPatch(typeof(Prefs), nameof(Prefs.Save))]
        public static class Prefs_Save
        {
            [HarmonyPriority(-100_000)]
            public static void Prefix()
            {
                ParaboxArchipelagoPlugin.State.StartMethod(nameof(Prefs_Save));
            }

            [HarmonyPriority(100_000)]
            public static void Postfix()
            {
                ParaboxArchipelagoPlugin.State.EndMethod(nameof(Prefs_Save));
            }
        }
    }
}