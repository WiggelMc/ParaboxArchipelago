using ParaboxArchipelago.State;
using UnityEngine;

namespace ParaboxArchipelago.Window
{
    public class LocationTrackerGameWindow : IGameWindow
    {
        public WindowState State
        {
            get => ParaboxArchipelagoPlugin.PrefState.LocationTrackerWindow;
            set => ParaboxArchipelagoPlugin.PrefState.LocationTrackerWindow = value;
        }

        public void DrawContent(Rect bounds, bool isInteractable, bool isOverlay)
        {
            
        }
    }
}