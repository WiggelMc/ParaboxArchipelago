using System;
using System.Linq;
using BepInEx;
using BepInEx.Logging;
using HarmonyLib;
using ParaboxArchipelago.Patches;

namespace ParaboxArchipelago
{
    [BepInPlugin(ParaboxPluginInfo.PLUGIN_GUID, ParaboxPluginInfo.PLUGIN_NAME, ParaboxPluginInfo.PLUGIN_VERSION)]
    [BepInProcess("Patrick's Parabox.exe")]
    public class ParaboxArchipelago : BaseUnityPlugin
    {
        internal static ManualLogSource Log;
        
        private void Awake()
        {
            Log = Logger;
            var harmony = new Harmony(ParaboxPluginInfo.PLUGIN_GUID);
            harmony.PatchAll();

            var originals = typeof(UnityEngine.Resources).GetMethods().Where(i => i.Name == "Load");
            var prefix =
                typeof(Resources.Resources_Load).GetMethod(nameof(Resources.Resources_Load.Prefix));
            var postfix =
                typeof(Resources.Resources_Load).GetMethod(nameof(Resources.Resources_Load.Postfix));
            foreach (var original in originals)
            {
                try
                {
                    harmony.Patch(original, new HarmonyMethod(prefix), new HarmonyMethod(postfix));
                }
                catch (Exception e)
                {
                    Logger.LogInfo(e);
                }
            }

            
            Logger.LogInfo($"Plugin {ParaboxPluginInfo.PLUGIN_GUID} is loaded!");
        }
    }
}
