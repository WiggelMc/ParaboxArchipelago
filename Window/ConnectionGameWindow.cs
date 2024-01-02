using System.Collections.Generic;
using System.Linq;
using ParaboxArchipelago.Patches;
using ParaboxArchipelago.State;
using UnityEngine;

namespace ParaboxArchipelago.Window
{
    public class ConnectionGameWindow : IGameWindow
    {
        public class KeyboardInputGroup : IControlKeyboardGroup
        {
            public string[] Items { get; } = new [] {
                nameof(MenuState.ConnectAddressInput),
                nameof(MenuState.ConnectSlotInput),
                nameof(MenuState.ConnectPasswordInput)
            }.Select(n => CommonMenuDrawing.TEXT_FIELD_PREFIX + n).ToArray();

            public void OnFinalItemConfirm()
            {
                Connect();
            }
        }
        public WindowState State
        {
            get => ParaboxArchipelagoPlugin.PrefState.ConnectionWindow;
            set => ParaboxArchipelagoPlugin.PrefState.ConnectionWindow = value;
        }

        public void DrawContent(Rect bounds, bool interactable)
        {
            var menuState = ParaboxArchipelagoPlugin.MenuState;
                    
            menuState.ConnectAddressInput = CommonMenuDrawing.DrawInputField(menuState.ConnectAddressInput, nameof(MenuState.ConnectAddressInput), new Rect(10, 10, 200, 20));
            menuState.ConnectSlotInput = CommonMenuDrawing.DrawInputField(menuState.ConnectSlotInput, nameof(MenuState.ConnectSlotInput), new Rect(10, 40, 200, 20));
            menuState.ConnectPasswordInput = CommonMenuDrawing.DrawInputField(menuState.ConnectPasswordInput, nameof(MenuState.ConnectPasswordInput), new Rect(10, 70, 200, 20));
            var connectPressed = CommonMenuDrawing.DrawButton("Connect", "connectButtonInput", new Rect(10, 120, 200, 20));

            if (connectPressed)
            {
                GUI.FocusControl("");
                Connect();
            }
            
            var gameCount = 200;
                    
            menuState.RecentGamesScrollPosition = GUI.BeginScrollView(new Rect(10, 200, 300, 500), menuState.RecentGamesScrollPosition, new Rect(0, 0, 260, gameCount*30+10), false, true);
            foreach (var i in Enumerable.Range(0,gameCount))
            {
                GUI.Button(new Rect(0, i*30+10, 260, 20), "Game " + i);
            }
            GUI.EndScrollView();
        }

        private static void Connect()
        {
            
        }
    }
    
    
}