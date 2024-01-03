using System.Linq;
using ParaboxArchipelago.GameOption;
using ParaboxArchipelago.Patches;
using ParaboxArchipelago.State;
using UnityEngine;

namespace ParaboxArchipelago.GameWindow
{
    public class ChatGameWindow : IGameWindow
    {
        public WindowState State
        {
            get => ParaboxArchipelagoPlugin.PrefState.ChatWindow;
            set => ParaboxArchipelagoPlugin.PrefState.ChatWindow = value;
        }

        public IGameOption[] Options { get; set; }

        public ChatGameWindow()
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