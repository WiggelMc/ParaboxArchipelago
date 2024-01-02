namespace ParaboxArchipelago.Patches
{
    public static class WorldAccessor
    {
        public enum GameWorldState
        {
            Playing,
            Paused,
            Other
        }
        
        public static GameWorldState GetWorldState()
        {
            return World.State switch
            {
                World.WS.Playing or World.WS.FastTravel => GameWorldState.Playing,
                World.WS.Paused => GameWorldState.Paused,
                _ => GameWorldState.Other
            };
        }
    }
}