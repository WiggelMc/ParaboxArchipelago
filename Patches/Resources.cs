using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using HarmonyLib;
using Mono.Cecil;
using MonoMod.Utils;
using UnityEngine;
using Object = UnityEngine.Object;

namespace ParaboxArchipelago.Patches
{
    public static class Resources
    {
        public class Prefs_GetPrefsFilePath
        {
            private static MethodBase TargetMethod() => AccessTools.Method(typeof(UnityEngine.Resources),
                nameof(UnityEngine.Resources.Load), new[] { typeof(string), typeof(Type) });
            public static bool Prefix(ref Object __result, string path, Type systemTypeInstance)
            {
                if (path == "hub")
                {
                    __result = new TextAsset("version 4\n#\n");
                    return false;
                }

                return true;
            }
            
            public static void Postfix(ref Object __result, string path, Type systemTypeInstance)
            {
                
            }
        }
    }
}