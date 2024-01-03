namespace ParaboxArchipelago.Patches
{
    public static class MenuStateAccessor
    {
        public static void OpenAPOptions()
        {
            ParaboxArchipelagoPlugin.MenuState.APOptionsPageEnabled = true;
        }

        public static void CloseApOptions()
        {
            Prefs.Save();
            ParaboxArchipelagoPlugin.MenuState.APOptionsPageEnabled = false;
        }
    }
}