namespace ParaboxArchipelago.State
{
    public class PrefState
    {
        public const string PERSONAL_ITEM_FEED_WINDOW_KEY = "ap_personal_item_feed_window";
        public WindowState PersonalItemFeedWindow { get; set; }
        public static WindowState PersonalItemFeedWindowDefault => new(0.2f, 0, 0.2f, 0.2f);
        
        
        
        public const string OTHER_ITEM_FEED_WINDOW_KEY = "ap_other_item_feed_window";
        public WindowState OtherItemFeedWindow { get; set; }
        public static WindowState OtherItemFeedWindowDefault => new(0.4f, 0, 0.2f, 0.2f);
        
        
        
        public const string CHAT_WINDOW_KEY = "ap_chat_window";
        public WindowState ChatWindow { get; set; }
        public static WindowState ChatWindowDefault => new(0.6f, 0, 0.2f, 0.2f);
        
        
        
        public const string ITEM_TRACKER_WINDOW_KEY = "ap_item_tracker_window";
        public WindowState ItemTrackerWindow { get; set; }
        public static WindowState ItemTrackerWindowDefault => new(0.8f, 0, 0.2f, 0.2f);
        
        
        
        public const string LOCATION_TRACKER_WINDOW_KEY = "ap_location_tracker_window";
        public WindowState LocationTrackerWindow { get; set; }
        public static WindowState LocationTrackerWindowDefault => new(0.8f, 0.2f, 0.2f, 0.2f);
        
        
        
        public const string CONNECTION_WINDOW_KEY = "ap_connection_window";
        public WindowState ConnectionWindow { get; set; }
        public static WindowState ConnectionWindowDefault => new(0, 0.2f, 0.2f, 0.2f);
    }
}