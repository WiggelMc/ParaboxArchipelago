﻿using ParaboxArchipelago.State;
using UnityEngine;

namespace ParaboxArchipelago.GameWindow
{
    public interface IGameWindow
    {
        WindowState State { get; set; }
        void DrawContent(Rect bounds, bool isInteractable, bool isOverlay);
        void DrawControls(Rect bounds);
    }
}