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
        public class Resources_Load
        {
            public static bool Prefix(ref Object __result, string path)
            {
                ParaboxArchipelago.Log.LogInfo("LOAD CALLED " + path);
                if (path == "hub")
                {
                    __result = new TextAsset("version 4\n#\n");
                    return false;
                }

                return true;
            }
            
            public static void Postfix(ref Object __result, string path)
            {
                
            }
        }
    }
}