using System.Linq;
using ParaboxArchipelago.GameOption;
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

        public IGameOption[] Options { get; set; }

        public ItemTrackerGameWindow()
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