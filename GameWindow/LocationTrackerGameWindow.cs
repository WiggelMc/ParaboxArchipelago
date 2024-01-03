﻿using ParaboxArchipelago.Patches;
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

        public void DrawContent(Rect bounds, bool isInteractable, bool isOverlay)
        {
            
        }

        public void DrawControls(Rect bounds)
        {
            CommonMenuDrawing.DrawCommonMenuControls(bounds, this);
        }
    }
}