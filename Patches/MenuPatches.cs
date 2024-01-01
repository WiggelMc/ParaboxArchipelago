using System;
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

                    var gameCount = 200;
                    
                    menuState.RecentGamesScrollPosition = GUI.BeginScrollView(new Rect(10, 200, 300, 500), menuState.RecentGamesScrollPosition, new Rect(0, 0, 260, gameCount*30+10), false, true);
                    foreach (var i in Enumerable.Range(0,gameCount))
                    {
                        GUI.Button(new Rect(0, i*30+10, 260, 20), "Game " + i);
                    }
                    GUI.EndScrollView();
                    
                    
                    menuState.ConnectionWindowRect.RelativeRect = DrawElement(menuState.ConnectionWindowRect.RelativeRect, bounds =>
                    {
                        
                    }, true, 0);
                    
                    
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

        
        private static readonly Texture2D InvisibleTexture;
        private static readonly GUIStyle InvisibleStyle;
        
        private static readonly Texture2D DragRectTexture;
        private static readonly GUIStyle DragRectStyle;
        
        private static readonly Texture2D DragWindowTexture;
        private static readonly GUIStyle DragWindowStyle;
        static MenuPatches()
        {
            var invisibleTexture = new Texture2D(1,1);
            invisibleTexture.SetPixel(1,1,new Color(0,0,0,0));
            InvisibleTexture = invisibleTexture;
            InvisibleStyle = GUIStyle.none;
            
            var dragRectTexture = new Texture2D(1,1);
            dragRectTexture.SetPixel(1,1,new Color(1,1,1,0.2f));
            DragRectTexture = dragRectTexture;
            DragRectStyle = new GUIStyle
            {
                border = new RectOffset(),
                normal = new GUIStyleState
                {
                    background = dragRectTexture
                }
            };
            
            var dragWindowTexture = new Texture2D(1,1);
            dragWindowTexture.SetPixel(1,1,new Color(0,0,0,0.2f));
            DragWindowTexture = dragWindowTexture;
            DragWindowStyle = new GUIStyle()
            {
                normal = new GUIStyleState()
                {
                    background = dragWindowTexture
                }
            };
        }
        
        
        
        private static Rect DrawElement(Rect relativeBounds, Action<Rect> drawContent, bool drawWindow, int windowID)
        {
            var actualWindowID = windowID * 20;
            var screenWidth = Draw.screenWidth;
            var screenHeight = Draw.screenHeight;
            var absoluteBounds = ToAbsolute(relativeBounds, screenWidth, screenHeight);
            
            Rect newRelativeBounds;
            if (drawWindow)
            {
                var newAbsoluteBounds1 = DrawWindowOuterControls(absoluteBounds, actualWindowID, screenWidth, screenHeight);
                var newAbsoluteBounds = GUI.Window(actualWindowID, newAbsoluteBounds1, _ =>
                {
                    DrawWindowControls(absoluteBounds);
                    drawContent.Invoke(absoluteBounds);
                }, DragWindowTexture, DragWindowStyle);
                
                newRelativeBounds = ToRelative(newAbsoluteBounds, screenWidth, screenHeight);
            }
            else
            {
                drawContent.Invoke(absoluteBounds);
                newRelativeBounds = relativeBounds;
            }

            return ClampRelative(newRelativeBounds);
        }

        private static Rect ToRelative(Rect absolute, float referenceWidth, float referenceHeight)
        {
            return new Rect(
                absolute.x / referenceWidth,
                absolute.y / referenceHeight,
                absolute.width / referenceWidth,
                absolute.height / referenceHeight
            );
        }
        
        private static Rect ToAbsolute(Rect relative, float referenceWidth, float referenceHeight)
        {
            return new Rect(
                relative.x * referenceWidth,
                relative.y * referenceHeight,
                relative.width * referenceWidth,
                relative.height * referenceHeight
            );
        }
        
        private static Rect ClampRelative(Rect relative)
        {
            return new Rect(
                Mathf.Clamp(relative.x, 0, 1 - relative.width),
                Mathf.Clamp(relative.y, 0, 1 - relative.height),
                Mathf.Clamp(relative.width, 0.1f, 1),
                Mathf.Clamp(relative.height, 0.1f, 1)
            );
        }

        private const int DRAG_MARGIN = 20;
        private const int DRAG_INSET = 20;
        
        private static void DrawWindowControls(Rect absoluteBounds)
        {
            var dragRects = new[]
            {
                new Rect(DRAG_INSET, DRAG_INSET, absoluteBounds.width - 2*DRAG_INSET, DRAG_MARGIN),
                new Rect(DRAG_INSET, absoluteBounds.height - DRAG_MARGIN - DRAG_INSET, absoluteBounds.width - 2*DRAG_INSET, DRAG_MARGIN),
                new Rect(DRAG_INSET, DRAG_MARGIN + DRAG_INSET, DRAG_MARGIN, absoluteBounds.height - 2*DRAG_MARGIN - 2*DRAG_INSET),
                new Rect(absoluteBounds.width - DRAG_MARGIN - DRAG_INSET, DRAG_MARGIN + DRAG_INSET, DRAG_MARGIN, absoluteBounds.height - 2*DRAG_MARGIN - 2*DRAG_INSET)
            };
            foreach (var dragRect in dragRects)
            {
                GUI.Box(dragRect, DragRectTexture, DragRectStyle);
                GUI.DragWindow(dragRect);
            }

            
        }

        private const int RESIZE_MARGIN = 5;
        
        private static Rect DrawWindowOuterControls(Rect absoluteBounds, int id, float referenceWidth, float referenceHeight)
        {
            
            var resizeRect = new Rect(
                absoluteBounds.x + absoluteBounds.width,
                absoluteBounds.y,
                RESIZE_MARGIN,
                absoluteBounds.height
            );

            var newResizeRect = GUI.Window(id - 1, resizeRect, _ =>
            {
                var rect = new Rect(
                    0, 0, resizeRect.width, resizeRect.height
                );
                GUI.DragWindow(rect);
                GUI.Box(rect, DragWindowTexture, DragRectStyle);
            }, DragWindowTexture, DragRectStyle);

            var offset = newResizeRect.x - resizeRect.x;
            
            return new Rect(
                absoluteBounds.x, absoluteBounds.y,
                Mathf.Clamp(absoluteBounds.x + absoluteBounds.width + offset, 0.1f * referenceWidth, referenceWidth) - absoluteBounds.x,
                absoluteBounds.height
            );
        }

        private static GUIStyle InitGUIStyle()
        {
            var guiStyle = new GUIStyle();
            
            
            return guiStyle;
        }
    }
}