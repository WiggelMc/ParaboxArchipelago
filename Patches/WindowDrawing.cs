using System;
using ParaboxArchipelago.State;
using ParaboxArchipelago.Window;
using UnityEngine;

namespace ParaboxArchipelago.Patches
{
    public static class WindowDrawing
    {
        private const int DRAG_MARGIN = 20;
        private const int DRAG_INSET = 20;
        private const int RESIZE_MARGIN = 10;
        private const int RESIZE_EDGE = 15;

        public static void DrawWindow(IGameWindow window, int id)
        {
            var state = World.State switch
            {
                World.WS.Playing => window.State.OverlayState,
                World.WS.Paused => window.State.MenuState,
                _ => WindowState.WindowInteractionState.Hidden
            };
            
            var drawWindow = ParaboxArchipelagoPlugin.MenuState.APOptionsPageEnabled;
            var interactable = state == WindowState.WindowInteractionState.Interact && !drawWindow;
            
            if (state == WindowState.WindowInteractionState.Hidden && !drawWindow) return;
            
            window.State.RelativeRect = DrawDraggableWindow(window.State.RelativeRect, bounds => window.DrawContent(bounds, interactable), drawWindow, id);
        }

        private static Rect DrawDraggableWindow(Rect relativeBounds, Action<Rect> drawContent, bool drawWindow, int windowID)
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
                }, MenuStyle.DragWindowTexture, MenuStyle.DragWindowStyle);
                
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
                GUI.Box(dragRect, MenuStyle.DragRectTexture, MenuStyle.DragRectStyle);
                GUI.DragWindow(dragRect);
            }
        }

        private static Rect DrawWindowOuterControls(Rect absoluteBounds, int id, float referenceWidth, float referenceHeight)
        {
            var resizeRects = new[]
            {
                new Rect( absoluteBounds.x + RESIZE_EDGE, absoluteBounds.y, absoluteBounds.width - 2*RESIZE_EDGE, RESIZE_MARGIN),
                new Rect( absoluteBounds.x + RESIZE_EDGE, absoluteBounds.y + absoluteBounds.height - RESIZE_MARGIN, absoluteBounds.width - 2*RESIZE_EDGE, RESIZE_MARGIN),
                new Rect(absoluteBounds.x, absoluteBounds.y + RESIZE_EDGE, RESIZE_MARGIN, absoluteBounds.height - 2*RESIZE_EDGE),
                new Rect(absoluteBounds.x + absoluteBounds.width - RESIZE_MARGIN, absoluteBounds.y + RESIZE_EDGE, RESIZE_MARGIN, absoluteBounds.height - 2*RESIZE_EDGE)
            };
            var offsets = new float[resizeRects.Length];

            for (var i = 0; i < resizeRects.Length; i++)
            {
                var resizeRect = resizeRects[i];
                var newID = id + 1 + i;
                var newResizeRect = GUI.Window(newID, resizeRect, _ =>
                {
                    var rect = new Rect(
                        0, 0, resizeRect.width, resizeRect.height
                    );
                    GUI.DragWindow(rect);
                    GUI.Box(rect, MenuStyle.DragWindowTexture, MenuStyle.DragRectStyle);
                }, MenuStyle.DragWindowTexture, MenuStyle.DragRectStyle);
                GUI.BringWindowToFront(newID);

                switch (i)
                {
                    case 0:
                    case 1:
                        offsets[i] = newResizeRect.y - resizeRect.y;
                        break;
                    case 2:
                    case 3:
                        offsets[i] = newResizeRect.x - resizeRect.x;
                        break;
                }
                
            }
            
            var newX = Mathf.Clamp(absoluteBounds.x + offsets[2], 0, absoluteBounds.xMax - 0.1f * referenceWidth);
            var newY = Mathf.Clamp(absoluteBounds.y + offsets[0], 0, absoluteBounds.yMax - 0.1f * referenceHeight);
            var newXMax = Mathf.Clamp(absoluteBounds.xMax + offsets[3], absoluteBounds.x + 0.1f * referenceWidth, referenceWidth);
            var newYMax = Mathf.Clamp(absoluteBounds.yMax + offsets[1], absoluteBounds.y + 0.1f * referenceHeight, referenceHeight);
            
            return new Rect(newX, newY, newXMax - newX, newYMax - newY);
        }
    }
}