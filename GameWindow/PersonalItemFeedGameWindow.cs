using ParaboxArchipelago.Patches;
using ParaboxArchipelago.State;
using UnityEngine;

namespace ParaboxArchipelago.GameWindow
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

        public void DrawControls(Rect bounds)
        {
            CommonMenuDrawing.DrawCommonMenuControls(bounds, this);
        }
    }
}