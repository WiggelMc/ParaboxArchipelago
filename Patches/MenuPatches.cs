using HarmonyLib;
using ParaboxArchipelago.State;
using UnityEngine;

namespace ParaboxArchipelago.Patches
{
    public static class MenuPatches
    {
        [HarmonyPatch(typeof(PauseMenu), nameof(PauseMenu.Update))]
        public static class PauseMenu_Update
        {
            public static bool Prefix()
            {
                return !ParaboxArchipelagoPlugin.MenuState.IsInTextField;
            }
        }
        
        [HarmonyPatch(typeof(World), "OnGUI")]
        public static class World_OnGUI
        {
            public static void Postfix()
            {
                if (World.State == World.WS.Paused)
                {
                    DrawInputField();
                }
            }
        }

        private static GUIStyle _guiStyle = InitGUIStyle();
        private static string _textFieldPrefix = "APTextField";
        
        private static void DrawInputField()
        {
            var menuState = ParaboxArchipelagoPlugin.MenuState;
            GUI.SetNextControlName(_textFieldPrefix + nameof(MenuState.ConnectSlotInput));
            menuState.ConnectSlotInput = GUI.TextField(new Rect(10, 10, 200, 20), menuState.ConnectSlotInput);
            
            menuState.IsInTextField = GUI.GetNameOfFocusedControl().StartsWith(_textFieldPrefix);
        }

        private static GUIStyle InitGUIStyle()
        {
            var guiStyle = new GUIStyle();
            
            
            
            return guiStyle;
        }
    }
}