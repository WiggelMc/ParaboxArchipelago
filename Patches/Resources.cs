using System;
using System.Linq;
using HarmonyLib;
using UnityEngine;
using Object = UnityEngine.Object;

namespace ParaboxArchipelago.Patches
{
    public static class Resources
    {
        public class Resources_Load
        {
            public static void Patch(Harmony harmony)
            {
                var methods = typeof(UnityEngine.Resources).GetMethods()
                    .Where(
                        i => i.Name == "Load"
                             && i.ReturnType == typeof(Object)
                             && i.GetParameters().Select(p => p.ParameterType).SequenceEqual(new []{typeof(string)})
                    );
                
                var type = typeof(Resources_Load);
                var prefix = type.GetMethod(nameof(Prefix));
                var postfix = type.GetMethod(nameof(Postfix));
                foreach (var method in methods)
                {
                    harmony.Patch(method, new HarmonyMethod(prefix), new HarmonyMethod(postfix));
                }
            }
            
            public static bool Prefix(ref Object __result, string path)
            {
                ParaboxArchipelagoPlugin.Log.LogInfo("LOAD CALLED " + path);
                if (path == "levels/hub")
                {
                    ParaboxArchipelagoPlugin.Log.LogInfo("LOAD HUB " + ParaboxResources.hub);
                    __result = new TextAsset(ParaboxResources.hub);
                    return false;
                }

                return true;
            }
            
            public static void Postfix(ref Object __result, string path)
            {
                if (path == "localization")
                {
                    var text = ((__result as TextAsset)!).text;
                    var newText = text + "\n" + ParaboxResources.local;
                    ParaboxArchipelagoPlugin.Log.LogInfo("LOAD LOCAL " + newText);
                    __result = new TextAsset(newText);
                }
            }
        }

        [HarmonyPatch(typeof(UnityEngine.Resources), nameof(UnityEngine.Resources.Load), typeof(string), typeof(Type))]
        public class Resources_Load_Type
        {
            public static bool Prefix(ref Object __result, string path, Type systemTypeInstance)
            {
                ParaboxArchipelagoPlugin.Log.LogInfo("LOAD CALLED " + path + " TYPE " + systemTypeInstance);
                return true;
            }
            
            public static void Postfix(ref Object __result, string path, Type systemTypeInstance)
            {
                
            }
        }
    }
}