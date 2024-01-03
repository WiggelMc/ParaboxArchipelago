using ParaboxArchipelago.Patches;
using ParaboxArchipelago.State;
using UnityEngine;

namespace ParaboxArchipelago.GameWindow
{
    public class ChatGameWindow : IGameWindow
    {
        public WindowState State
        {
            get => ParaboxArchipelagoPlugin.PrefState.ChatWindow;
            set => ParaboxArchipelagoPlugin.PrefState.ChatWindow = value;
        }

        public void DrawContent(Rect bounds, bool isInteractable, bool isOverlay)
        {
            
        }

        public void DrawControls(Rect bounds)
        {
            CommonMenuDrawing.DrawCommonMenuControls(bounds, this);
        }
    }
}