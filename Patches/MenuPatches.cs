using System.Collections.Generic;
using System.Linq;
using HarmonyLib;
using ParaboxArchipelago.State;
using UnityEngine;
using UnityEngine.InputSystem;

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

        private static void OnConnectPress()
        {
            GUI.FocusControl("");
        }
        
        [HarmonyPatch(typeof(World), "OnGUI")]
        public static class World_OnGUI
        {
            private static readonly List<string> MenuNames = new[]
            {
                nameof(MenuState.ConnectAddressInput),
                nameof(MenuState.ConnectSlotInput),
                nameof(MenuState.ConnectPasswordInput)
            }.Select(n => TextFieldPrefix + n).ToList();
            
            public static void Postfix()
            {
                if (World.State == World.WS.Paused)
                {
                    var menuState = ParaboxArchipelagoPlugin.MenuState;
                    
                    menuState.ConnectAddressInput = DrawInputField(menuState.ConnectAddressInput, nameof(MenuState.ConnectAddressInput), new Rect(10, 10, 200, 20));
                    menuState.ConnectSlotInput = DrawInputField(menuState.ConnectSlotInput, nameof(MenuState.ConnectSlotInput), new Rect(10, 40, 200, 20));
                    menuState.ConnectPasswordInput = DrawInputField(menuState.ConnectPasswordInput, nameof(MenuState.ConnectPasswordInput), new Rect(10, 70, 200, 20));
                    var connectPressed = DrawButton("Connect", "connectButtonInput", new Rect(10, 120, 200, 20));

                    var focusedControlName = GUI.GetNameOfFocusedControl();
                    menuState.IsInTextField = focusedControlName.StartsWith(TextFieldPrefix);
                    
                    if (Keyboard.current.enterKey.wasReleasedThisFrame)
                        menuState.GuiKeyLastPressTime = Time.time;
                    if (connectPressed)
                    {
                        OnConnectPress();
                        return;
                    }
                    if (Keyboard.current.escapeKey.wasPressedThisFrame)
                    {
                        GUI.FocusControl("");
                        return;
                    }
                    if (menuState.GuiKeyLastPressTime > Time.time) return;
                    var focusIndex = MenuNames.IndexOf(focusedControlName);
                    
                    if (menuState.IsInTextField && Keyboard.current.enterKey.wasPressedThisFrame)
                    {
                        ParaboxArchipelagoPlugin.Log.LogInfo("Enter Pressed");
                        menuState.GuiKeyLastPressTime = Time.time + 1f;
                        if (focusIndex < 2)
                            GUI.FocusControl(MenuNames[focusIndex + 1]);
                        else
                            OnConnectPress();
                    }
                }
            }
        }

        private static readonly GUIStyle GUIStyle = InitGUIStyle();
        private static readonly string TextFieldPrefix = "APTextField";
        
        private static string DrawInputField(string value, string name, Rect position)
        {
            GUI.SetNextControlName(TextFieldPrefix + name);
            return GUI.TextField(position, value);
        }

        private static bool DrawButton(string text, string name, Rect position)
        {
            GUI.SetNextControlName(name);
            return GUI.Button(position, text);
        }

        private static GUIStyle InitGUIStyle()
        {
            var guiStyle = new GUIStyle();
            
            
            return guiStyle;
        }
    }
}