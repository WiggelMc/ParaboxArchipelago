using BepInEx;

namespace ParaboxArchipelago
{
    [BepInPlugin(ParaboxPluginInfo.PLUGIN_GUID, ParaboxPluginInfo.PLUGIN_NAME, ParaboxPluginInfo.PLUGIN_VERSION)]
    [BepInProcess("Patrick's Parabox.exe")]
    public class ParaboxArchipelago : BaseUnityPlugin
    {
        private void Awake()
        {
            // Plugin startup logic
            Logger.LogInfo($"Plugin {ParaboxPluginInfo.PLUGIN_GUID} is loaded!");
        }
    }
}
