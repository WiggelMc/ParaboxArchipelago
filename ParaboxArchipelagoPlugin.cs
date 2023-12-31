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
        internal static ParaboxState State;
        
        private void Awake()
        {
            Log = Logger;
            State = new ParaboxState();
            var harmony = new Harmony(ParaboxPluginInfo.PLUGIN_GUID);
            harmony.PatchAll();
            ResourcesPatches.Resources_Load.Patch(harmony);
            MusicPatches.FMODSquare_AreaNameToFMODIndex.Patch();
            
            Log.LogInfo($"Plugin {ParaboxPluginInfo.PLUGIN_GUID} is loaded!");
        }
    }
}
