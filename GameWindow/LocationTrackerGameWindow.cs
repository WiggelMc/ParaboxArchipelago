using System.Linq;
using ParaboxArchipelago.GameOption;
using ParaboxArchipelago.Patches;
using ParaboxArchipelago.State;
using UnityEngine;

namespace ParaboxArchipelago.GameWindow
{
    public class LocationTrackerGameWindow : IGameWindow
    {
        public WindowState State
        {
            get => ParaboxArchipelagoPlugin.PrefState.LocationTrackerWindow;
            set => ParaboxArchipelagoPlugin.PrefState.LocationTrackerWindow = value;
        }

        public IGameOption[] Options { get; set; }

        public LocationTrackerGameWindow()
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