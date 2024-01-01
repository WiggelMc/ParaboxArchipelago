using ParaboxArchipelago.State;
using UnityEngine;

namespace ParaboxArchipelago.Window
{
    public class OtherItemFeedGameWindow : IGameWindow
    {
        public WindowState State
        {
            get => ParaboxArchipelagoPlugin.PrefState.OtherItemFeedWindow;
            set => ParaboxArchipelagoPlugin.PrefState.OtherItemFeedWindow = value;
        }

        public void DrawContent(Rect bounds, bool interactable)
        {
            
        }
    }
}