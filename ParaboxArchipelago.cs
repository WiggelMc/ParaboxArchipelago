using BepInEx;
using BepInEx.Logging;
using HarmonyLib;

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
            new Harmony(ParaboxPluginInfo.PLUGIN_GUID).PatchAll();
            Logger.LogInfo($"Plugin {ParaboxPluginInfo.PLUGIN_GUID} is loaded!");
        }
    }
}
