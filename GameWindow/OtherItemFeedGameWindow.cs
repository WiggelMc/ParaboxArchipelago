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
    }
}