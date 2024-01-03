using System.Text;
using UnityEngine;

namespace ParaboxArchipelago.State
{
    public class WindowState
    {
        public Rect RelativeRect { get; set; }
        public WindowInteractionState MenuState { get; set; }
        public WindowInteractionState OverlayState { get; set; }

        public WindowState(float x, float y, float width, float height)
        {
            RelativeRect = new Rect(x, y, width, height);
            MenuState = WindowInteractionState.Hidden;
            OverlayState = WindowInteractionState.Hidden;
        }

        public WindowState(Rect relativeRect, WindowInteractionState menuState, WindowInteractionState overlayState)
        {
            RelativeRect = relativeRect;
            MenuState = menuState;
            OverlayState = overlayState;
        }

        public WindowState Copy()
        {
            return new WindowState(RelativeRect, MenuState, OverlayState);
        }

        public override string ToString()
        {
            var builder = new StringBuilder();
            builder.Append((int)MenuState);
            builder.Append("|");
            builder.Append((int)OverlayState);
            builder.Append("|");
            builder.Append(RelativeRect.x);
            builder.Append("|");
            builder.Append(RelativeRect.y);
            builder.Append("|");
            builder.Append(RelativeRect.width);
            builder.Append("|");
            builder.Append(RelativeRect.height);
            return builder.ToString();
        }

        public static WindowState Parse(string str)
        {
            var components = str.Split('|');
            var menuState = (WindowInteractionState)int.Parse(components[0]);
            var overlayState = (WindowInteractionState)int.Parse(components[1]);
            var rectX = float.Parse(components[2]);
            var rectY = float.Parse(components[3]);
            var rectWidth = float.Parse(components[4]);
            var rectHeight = float.Parse(components[5]);
            return new WindowState(
                new Rect(rectX, rectY, rectWidth, rectHeight),
                menuState,
                overlayState
            );
        }
        
        public enum WindowInteractionState
        {
            Hidden = 0,
            View = 1,
            Interact = 2
        }
    }
}