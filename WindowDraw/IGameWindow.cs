using ParaboxArchipelago.State;
using UnityEngine;

namespace ParaboxArchipelago.WindowDraw
{
    public interface IGameWindow
    {
        WindowState State { get; set; }
        void DrawContent(Rect bounds, bool isInteractable, bool isOverlay);
    }
}