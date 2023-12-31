using System.Reflection;
using System.Resources;
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
    }
}