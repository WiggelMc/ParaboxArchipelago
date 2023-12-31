using BepInEx;
using BepInEx.Logging;
using HarmonyLib;
using ParaboxArchipelago.Patches;

namespace ParaboxArchipelago
{
    [BepInPlugin(ParaboxPluginInfo.PLUGIN_GUID, ParaboxPluginInfo.PLUGIN_NAME, ParaboxPluginInfo.PLUGIN_VERSION)]
    [BepInProcess("Patrick's Parabox.exe")]
    public class ParaboxArchipelagoPlugin : BaseUnityPlugin
    {
        internal static ManualLogSource Log;
        
        private void Awake()
        {
            Log = Logger;
            var harmony = new Harmony(ParaboxPluginInfo.PLUGIN_GUID);
            harmony.PatchAll();
            Resources.Resources_Load.Patch(harmony);
            
            Log.LogInfo($"Plugin {ParaboxPluginInfo.PLUGIN_GUID} is loaded!");
        }
    }
}
