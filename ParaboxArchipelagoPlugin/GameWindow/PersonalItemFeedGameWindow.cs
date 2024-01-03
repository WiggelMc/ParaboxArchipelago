using System.Linq;
using ParaboxArchipelago.GameOption;
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

        public IGameOption[] Options { get; set; }

        public PersonalItemFeedGameWindow()
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