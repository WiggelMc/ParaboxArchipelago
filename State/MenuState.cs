using System;
using UnityEngine;

namespace ParaboxArchipelago.State
{
    public partial class MenuState
    {
        public string ConnectSlotInput { get; set; } = "";
        public string ConnectAddressInput { get; set; } = "";
        public string ConnectPasswordInput { get; set; } = "";
        public bool IsInTextField { get; set; } = false;
        public float GuiKeyLastPressTime { get; set; } = 0f;
        public Vector2 RecentGamesScrollPosition { get; set; } = Vector2.zero;
        public bool EnableWindowMove { get; set; } = false;

        public WindowState PersonalItemFeedWindow { get; set; } = PrefState.PersonalItemFeedWindowDefault;
        public WindowState OtherItemFeedWindow { get; set; } = PrefState.OtherItemFeedWindowDefault;
        public WindowState ChatWindow { get; set; } = PrefState.ChatWindowDefault;
        public WindowState ItemTrackerWindow { get; set; } = PrefState.ItemTrackerWindowDefault;
        public WindowState LocationTrackerWindow { get; set; } = PrefState.LocationTrackerWindowDefault;
        public WindowState ConnectionWindow { get; set; } = PrefState.ConnectionWindowDefault;
    }
}