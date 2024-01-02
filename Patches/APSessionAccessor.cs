using Archipelago.MultiClient.Net;
using Archipelago.MultiClient.Net.Enums;

namespace ParaboxArchipelago.Patches
{
    public static class APSessionAccessor
    {
        public static void Connect(string address, string slot, string password)
        {
            var apState = ParaboxArchipelagoPlugin.APState;
            var session = ArchipelagoSessionFactory.CreateSession(address);
            var apPassword = password != "" ? password : null;
            var loginResult = session.TryConnectAndLogin("Hollow Knight", slot, ItemsHandlingFlags.AllItems, password: apPassword);
            ParaboxArchipelagoPlugin.Log.LogInfo("CONNECTION: " + loginResult.Successful);
            
            apState.Session = session;
        }
    }
}