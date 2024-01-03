using ParaboxArchipelago.Patches;
using ParaboxArchipelago.State;
using UnityEngine;

namespace ParaboxArchipelago.GameWindow
{
    public class OtherItemFeedGameWindow : IGameWindow
    {
        public WindowState State
        {
            get => ParaboxArchipelagoPlugin.PrefState.OtherItemFeedWindow;
            set => ParaboxArchipelagoPlugin.PrefState.OtherItemFeedWindow = value;
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