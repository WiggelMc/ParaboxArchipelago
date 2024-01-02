using System;
using System.Collections.Generic;
using System.Linq;
using HarmonyLib;
using ParaboxArchipelago.State;
using ParaboxArchipelago.Window;
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
                var menuState = ParaboxArchipelagoPlugin.MenuState;
                return !(menuState.IsInTextField || menuState.APOptionsPageEnabled);
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
            }.Select(n => MenuPatches.TextFieldPrefix + n).ToList();

            private static readonly IGameWindow PersonalItemFeedGameWindow = new PersonalItemFeedGameWindow();
            private static readonly IGameWindow OtherItemFeedGameWindow = new OtherItemFeedGameWindow();
            private static readonly IGameWindow ChatGameWindow = new ChatGameWindow();
            private static readonly IGameWindow ItemTrackerGameWindow = new ItemTrackerGameWindow();
            private static readonly IGameWindow LocationTrackerGameWindow = new LocationTrackerGameWindow();
            private static readonly IGameWindow ConnectionGameWindow = new ConnectionGameWindow();
            public static void Postfix()
            {
                if (World.State == World.WS.Paused)
                {
                    var menuState = ParaboxArchipelagoPlugin.MenuState;
                    
                    menuState.ConnectAddressInput = MenuPatches.DrawInputField(menuState.ConnectAddressInput, nameof(MenuState.ConnectAddressInput), new Rect(10, 10, 200, 20));
                    menuState.ConnectSlotInput = MenuPatches.DrawInputField(menuState.ConnectSlotInput, nameof(MenuState.ConnectSlotInput), new Rect(10, 40, 200, 20));
                    menuState.ConnectPasswordInput = MenuPatches.DrawInputField(menuState.ConnectPasswordInput, nameof(MenuState.ConnectPasswordInput), new Rect(10, 70, 200, 20));
                    var connectPressed = MenuPatches.DrawButton("Connect", "connectButtonInput", new Rect(10, 120, 200, 20));

                    var gameCount = 200;
                    
                    menuState.RecentGamesScrollPosition = GUI.BeginScrollView(new Rect(10, 200, 300, 500), menuState.RecentGamesScrollPosition, new Rect(0, 0, 260, gameCount*30+10), false, true);
                    foreach (var i in Enumerable.Range(0,gameCount))
                    {
                        GUI.Button(new Rect(0, i*30+10, 260, 20), "Game " + i);
                    }
                    GUI.EndScrollView();

                    ////
                    ////
                    ////
                    ////
                    
                    var windowID = 0;
                    Patches.WindowDrawing.DrawWindow(PersonalItemFeedGameWindow, windowID++);
                    Patches.WindowDrawing.DrawWindow(OtherItemFeedGameWindow, windowID++);
                    Patches.WindowDrawing.DrawWindow(ChatGameWindow, windowID++);
                    Patches.WindowDrawing.DrawWindow(ItemTrackerGameWindow, windowID++);
                    Patches.WindowDrawing.DrawWindow(LocationTrackerGameWindow, windowID++);
                    Patches.WindowDrawing.DrawWindow(ConnectionGameWindow, windowID++);

                    GameMenuOverlay.DrawMenuOverlay();
                    
                    var focusedControlName = GUI.GetNameOfFocusedControl();
                    menuState.IsInTextField = focusedControlName.StartsWith(MenuPatches.TextFieldPrefix);
                    
                    /////
                    /////
                    /////
                    /////
                    
                    if (Keyboard.current.enterKey.wasReleasedThisFrame)
                        menuState.GuiKeyLastPressTime = Time.time;
                    if (connectPressed)
                    {
                        MenuPatches.OnConnectPress();
                        return;
                    }
                    if (Keyboard.current.escapeKey.wasPressedThisFrame)
                    {
                        if (menuState.IsInTextField)
                        {
                            GUI.FocusControl("");
                        }
                        else if (menuState.APOptionsPageEnabled)
                        {
                            MenuStateAccessor.CloseApOptions();
                        }
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
                            MenuPatches.OnConnectPress();
                    }
                }
            }
        }

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
    }
}