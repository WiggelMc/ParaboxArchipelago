using System;
using System.Collections.Generic;
using System.Linq;
using ParaboxArchipelago.Window;
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

        public static void HandleMenuControlInput()
        {
            var menuState = ParaboxArchipelagoPlugin.MenuState;
            
            var focusedControlName = GUI.GetNameOfFocusedControl();
            menuState.IsInTextField = focusedControlName.StartsWith(TEXT_FIELD_PREFIX);
            
            if (Keyboard.current.enterKey.wasReleasedThisFrame)
                menuState.GuiKeyLastPressTime = Time.time;
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
            
            
            var result = InputGroups
                .Select(group => new ControlSearchResult(group, Array.IndexOf(group.Items, focusedControlName)))
                .FirstOrDefault( r => r.Index != -1);
            
            ParaboxArchipelagoPlugin.Log.LogInfo(result.Index);
            
            if (result.Group != null)
            {
                if (menuState.IsInTextField && Keyboard.current.enterKey.wasPressedThisFrame)
                {
                    ParaboxArchipelagoPlugin.Log.LogInfo("Enter Pressed");
                    menuState.GuiKeyLastPressTime = Time.time + 1f;
                    if (result.Index < result.Group.Items.Length - 1)
                        GUI.FocusControl(result.Group.Items[result.Index + 1]);
                    else
                        GUI.FocusControl("");
                
                }
            }
        }
    }
}