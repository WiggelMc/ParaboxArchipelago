using System;
using System.Collections.Generic;
using System.Linq;
using ParaboxArchipelago.GameOption;
using ParaboxArchipelago.GameWindow;
using ParaboxArchipelago.State;
using ParaboxArchipelago.Style;
using UnityEngine;
using UnityEngine.InputSystem;

namespace ParaboxArchipelago.Patches
{
    public static class CommonMenuDrawing
    {
        public struct ControlSearchResult
        {
            public readonly IControlKeyboardGroup Group;
            public readonly int Index;

            public ControlSearchResult(IControlKeyboardGroup group, int index)
            {
                Group = group;
                Index = index;
            }
        }
        
        public const string TEXT_FIELD_PREFIX = "APTextField";

        public static readonly IControlKeyboardGroup[] InputGroups = {
            new ConnectionGameWindow.KeyboardInputGroup()
        };

        public static string DrawInputField(string value, string name, Rect position)
        {
            GUI.SetNextControlName(TEXT_FIELD_PREFIX + name);
            return GUI.TextField(position, value);
        }

        public static bool DrawButton(string text, string name, Rect position)
        {
            GUI.SetNextControlName(name);
            return GUI.Button(position, text);
        }
        
        public static IGameOption[] GetCommonGameOptions(IGameWindow window)
        {
            return new IGameOption[] {
                new RadioOption(
                    "Menu",
                    new []{"Hidden", "View", "Interactable"},
                    v => window.State.MenuState = (WindowState.WindowInteractionState)v,
                    () => (int)window.State.MenuState
                ),
                new RadioOption(
                    "Overlay",
                    new []{"Hidden", "View", "Interactable"},
                    v => window.State.OverlayState = (WindowState.WindowInteractionState)v,
                    () => (int)window.State.OverlayState
                )
            };
        }

        public static void DrawMenuControls(Rect bounds, IGameWindow window)
        {
            GUILayout.BeginArea(bounds);
            GUILayout.FlexibleSpace();
            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            GUILayout.BeginVertical(MenuStyle.SolidDarkStyle);
            foreach (var gameOption in window.Options)
            {
                gameOption.Draw();
            }
            GUILayout.EndVertical();
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();
            GUILayout.FlexibleSpace();
            GUILayout.EndArea();
        }

        public static void HandleMenuControlInput()
        {
            var menuState = ParaboxArchipelagoPlugin.MenuState;

            var escapePressed = Keyboard.current.escapeKey.isPressed && !menuState.EscapeKeyPressed;
            var enterPressed = Keyboard.current.enterKey.isPressed && !menuState.EnterKeyPressed;
            UpdateInput();
            
            var focusedControlName = GUI.GetNameOfFocusedControl();
            menuState.IsInTextField = focusedControlName.StartsWith(TEXT_FIELD_PREFIX);
            
            if (escapePressed)
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

            if (enterPressed && menuState.IsInTextField)
            {
                var result = InputGroups
                    .Select(group => new ControlSearchResult(group, Array.IndexOf(group.Items, focusedControlName)))
                    .FirstOrDefault( r => r.Index != -1);
            
                ParaboxArchipelagoPlugin.Log.LogInfo(result.Index);
            
                if (result.Group != null)
                {
                    if (result.Index < result.Group.Items.Length - 1)
                        GUI.FocusControl(result.Group.Items[result.Index + 1]);
                    else
                        GUI.FocusControl("");
                }
            }
        }

        public static void UpdateInput()
        {
            var menuState = ParaboxArchipelagoPlugin.MenuState;
            
            menuState.EnterKeyPressed = Keyboard.current.enterKey.isPressed;
            menuState.EscapeKeyPressed = Keyboard.current.escapeKey.isPressed;
        }
    }
}