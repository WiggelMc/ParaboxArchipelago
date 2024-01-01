﻿using UnityEngine;

namespace ParaboxArchipelago.State
{
    public class MenuState
    {
        public string ConnectSlotInput { get; set; } = "";
        public string ConnectAddressInput { get; set; } = "";
        public string ConnectPasswordInput { get; set; } = "";
        public bool IsInTextField { get; set; } = false;
        public float GuiKeyLastPressTime { get; set; } = 0f;
        public Vector2 RecentGamesScrollPosition { get; set; } = Vector2.zero;
        public bool EnableWindowMove { get; set; } = false;

        public WindowState PersonalItemFeedWindowRect { get; set; } = new(0.2f, 0, 0.2f, 0.2f);
        public WindowState OtherItemFeedWindowRect { get; set; } = new(0.4f, 0, 0.2f, 0.2f);
        public WindowState ChatWindowRect { get; set; } = new(0.6f, 0, 0.2f, 0.2f);
        public WindowState ItemTrackerWindowRect { get; set; } = new(0.8f, 0, 0.2f, 0.2f);
        public WindowState LocationTrackerWindowRect { get; set; } = new(0.8f, 0.2f, 0.2f, 0.2f);
        public WindowState ConnectionWindowRect { get; set; } = new(0, 0.2f, 0.2f, 0.2f);
        
        public class WindowState
        {
            public Rect RelativeRect { get; set; }
            public WindowInteractionState MenuState { get; set; }
            public WindowInteractionState OverlayState { get; set; }

            public WindowState(float x, float y, float width, float height)
            {
                RelativeRect = new Rect(x, y, width, height);
                MenuState = WindowInteractionState.Hidden;
                OverlayState = WindowInteractionState.Hidden;
            }
        }
        
        public enum WindowInteractionState
        {
            Hidden, View, Interact
        }
    }
}