using System;
using System.Collections.Generic;
using System.Linq;
using HarmonyLib;
using ParaboxArchipelago.State;
using ParaboxArchipelago.WindowDraw;
using UnityEngine;
using UnityEngine.InputSystem;
// ReSharper disable RedundantAssignment

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
        
        [HarmonyPatch(typeof(World), "UpdateInner")]
        public static class World_UpdateInner
        {
            public static bool Prefix()
            {
                var menuState = ParaboxArchipelagoPlugin.MenuState;
                var isPaused = WorldAccessor.GetWorldState() == WorldAccessor.GameWorldState.Paused;
                return isPaused || !(menuState.IsInTextField);
            }
        }
        
        [HarmonyPatch(typeof(World), "OnGUI")]
        public static class World_OnGUI
        {
            private static readonly List<string> MenuNames = new[]
            {
                nameof(MenuState.ConnectAddressInput),
                nameof(MenuState.ConnectSlotInput),
                nameof(MenuState.ConnectPasswordInput)
            }.Select(n => CommonMenuDrawing.TEXT_FIELD_PREFIX + n).ToList();

            private static readonly IGameWindow PersonalItemFeedGameWindow = new PersonalItemFeedGameWindow();
            private static readonly IGameWindow OtherItemFeedGameWindow = new OtherItemFeedGameWindow();
            private static readonly IGameWindow ChatGameWindow = new ChatGameWindow();
            private static readonly IGameWindow ItemTrackerGameWindow = new ItemTrackerGameWindow();
            private static readonly IGameWindow LocationTrackerGameWindow = new LocationTrackerGameWindow();
            private static readonly IGameWindow ConnectionGameWindow = new ConnectionGameWindow();
            public static void Postfix()
            {
                var windowID = 0;
                WindowDrawing.DrawWindow(PersonalItemFeedGameWindow, windowID++);
                WindowDrawing.DrawWindow(OtherItemFeedGameWindow, windowID++);
                WindowDrawing.DrawWindow(ChatGameWindow, windowID++);
                WindowDrawing.DrawWindow(ItemTrackerGameWindow, windowID++);
                WindowDrawing.DrawWindow(LocationTrackerGameWindow, windowID++);
                WindowDrawing.DrawWindow(ConnectionGameWindow, windowID++);
                
                if (World.State == World.WS.Paused)
                {
                    GameMenuOverlay.DrawMenuOverlay();
                }
                
                CommonMenuDrawing.HandleMenuControlInput();
            }
        }
    }
}