using BepInEx;

namespace ParaboxArchipelago
{
    [BepInPlugin(ParaboxPluginInfo.PluginGuid, ParaboxPluginInfo.PluginName, ParaboxPluginInfo.PluginVersion)]
    [BepInProcess("Patrick's Parabox.exe")]
    public class ParaboxArchipelago : BaseUnityPlugin
    {
        private void Awake()
        {
            // Plugin startup logic
            Logger.LogInfo($"Plugin {ParaboxPluginInfo.PluginGuid} is loaded!");
        }
    }
}
