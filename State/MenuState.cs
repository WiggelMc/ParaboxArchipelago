using UnityEngine;

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
        public Rect TestWindowRect { get; set; } = new(300, 10, 200, 200);
    }
}