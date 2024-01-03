using ParaboxArchipelago.Patches;
using ParaboxArchipelago.State;
using UnityEngine;

namespace ParaboxArchipelago.GameWindow
{
    public class ItemTrackerGameWindow : IGameWindow
    {
        public WindowState State
        {
            get => ParaboxArchipelagoPlugin.PrefState.ItemTrackerWindow;
            set => ParaboxArchipelagoPlugin.PrefState.ItemTrackerWindow = value;
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