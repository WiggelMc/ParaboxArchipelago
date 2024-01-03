using System.Linq;
using ParaboxArchipelago.GameOption;
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

        public IGameOption[] Options { get; set; }

        public OtherItemFeedGameWindow()
        {
            Options = CommonMenuDrawing.GetCommonGameOptions(this).Concat(new IGameOption[]
            {
                
            }).ToArray();
        }

        public void DrawContent(Rect bounds, bool isInteractable, bool isOverlay)
        {
            
        }
    }
}