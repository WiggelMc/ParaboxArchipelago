using System;
using System.Linq;
using HarmonyLib;
using UnityEngine;
using Object = UnityEngine.Object;

namespace ParaboxArchipelago.Patches
{
    public static class ResourcesPatches
    {
        public static class Resources_Load
        {
            public static void Patch(Harmony harmony)
            {
                var methods = typeof(Resources).GetMethods()
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
                switch (path)
                {
                    case "levels/hub":
                        ParaboxArchipelagoPlugin.Log.LogInfo("LOAD HUB " + ParaboxResources.hub);
                        __result = new TextAsset(ParaboxResources.hub);
                        return false;
                    default:
                        return true;
                }
            }
            
            public static void Postfix(ref Object __result, string path)
            {
                switch (path)
                {
                    case "localization":
                    {
                        var originalLocal = ((__result as TextAsset)!).text;
                        var newLocal = originalLocal + "\n" + ParaboxResources.local;
                        ParaboxArchipelagoPlugin.Log.LogInfo("LOAD LOCAL " + newLocal);
                        __result = new TextAsset(newLocal);
                        break;
                    }
                    case "puzzle_data":
                        var originalPuzzleData = ((__result as TextAsset)!).text;
                        var newPuzzleData = originalPuzzleData.Replace("a1", "a1.1Fool");
                        ParaboxArchipelagoPlugin.Log.LogInfo("LOAD PUZZLE DATA " + newPuzzleData);
                        __result = new TextAsset(newPuzzleData);
                        break;
                }
            }
        }

        [HarmonyPatch(typeof(Resources), nameof(Resources.Load), typeof(string), typeof(Type))]
        public static class Resources_Load_Type
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