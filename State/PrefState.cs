namespace ParaboxArchipelago.State
{
    public class PrefState
    {
        public const string ENABLE_ITEM_TRACKER_KEY = "ap_enable_item_tracker";
        public bool EnableItemTracker { get; set; }
        
        public const string ENABLE_LOCATION_TRACKER_KEY = "ap_enable_location_tracker";
        public bool EnableLocationTracker { get; set; }
        
        public const string ENABLE_ITEM_TRACKER_OVERLAY_KEY = "ap_enable_item_tracker_overlay";
        public bool EnableItemTrackerOverlay { get; set; }
        
        public const string ENABLE_LOCATION_TRACKER_OVERLAY_KEY = "ap_enable_location_tracker_overlay";
        public bool EnableLocationTrackerOverlay { get; set; }
    }
}