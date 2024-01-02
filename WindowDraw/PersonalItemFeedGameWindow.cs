using ParaboxArchipelago.State;
using UnityEngine;

namespace ParaboxArchipelago.WindowDraw
{
    public class PersonalItemFeedGameWindow : IGameWindow
    {
        public WindowState State
        {
            get => ParaboxArchipelagoPlugin.PrefState.PersonalItemFeedWindow;
            set => ParaboxArchipelagoPlugin.PrefState.PersonalItemFeedWindow = value;
        }

        public void DrawContent(Rect bounds, bool isInteractable, bool isOverlay)
        {
            
        }
    }
}