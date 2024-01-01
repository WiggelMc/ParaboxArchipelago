using ParaboxArchipelago.State;
using UnityEngine;

namespace ParaboxArchipelago.Window
{
    public class ChatGameWindow : IGameWindow
    {
        public WindowState State
        {
            get => ParaboxArchipelagoPlugin.PrefState.ChatWindow;
            set => ParaboxArchipelagoPlugin.PrefState.ChatWindow = value;
        }

        public void DrawContent(Rect bounds, bool interactable)
        {
            
        }
    }
}