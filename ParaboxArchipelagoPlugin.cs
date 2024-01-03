using BepInEx;
using BepInEx.Logging;
using HarmonyLib;
using ParaboxArchipelago.Patches;
using ParaboxArchipelago.State;

namespace ParaboxArchipelago
{
    [BepInPlugin(ParaboxPluginInfo.PLUGIN_GUID, ParaboxPluginInfo.PLUGIN_NAME, ParaboxPluginInfo.PLUGIN_VERSION)]
    [BepInProcess("Patrick's Parabox.exe")]
    public class ParaboxArchipelagoPlugin : BaseUnityPlugin
    {
        internal static ManualLogSource Log;
        public static MethodState MethodState;
        public static PrefState PrefState;
        public static MenuState MenuState;
        public static APState APState;
        
        private void Awake()
        {
            Log = Logger;
            MethodState = new MethodState();
            PrefState = new PrefState();
            MenuState = new MenuState();
            APState = new APState();
            
            Harmony.DEBUG = true;
            var harmony = new Harmony(ParaboxPluginInfo.PLUGIN_GUID);
            harmony.PatchAll();
            ResourcesPatches.Resources_Load.Patch(harmony);
            MusicPatches.FMODSquare_AreaNameToFMODIndex.Patch();
            
            Log.LogInfo($"Plugin {ParaboxPluginInfo.PLUGIN_GUID} is loaded!");
        }
    }
}
