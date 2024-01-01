﻿using ParaboxArchipelago.State;
using UnityEngine;

namespace ParaboxArchipelago.Window
{
    public interface IGameWindow
    {
        WindowState State { get; set; }
        void DrawContent(Rect bounds, bool interactable);
    }
}