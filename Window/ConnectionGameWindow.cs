using ParaboxArchipelago.State;
using UnityEngine;

namespace ParaboxArchipelago.Window
{
    public class ConnectionGameWindow : IGameWindow
    {
        public WindowState State
        {
            get => ParaboxArchipelagoPlugin.PrefState.ConnectionWindow;
            set => ParaboxArchipelagoPlugin.PrefState.ConnectionWindow = value;
        }

        public void DrawContent(Rect bounds, bool interactable)
        {
            
        }
    }
}