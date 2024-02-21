using System;

namespace ParaboxArchipelago.LevelGenerator
{
    public abstract class ItemOption
    {
        public enum SingleOption
        {
            Disabled,
            Unlocked,
            Single
        }
        public enum ProgressiveOption
        {
            Disabled,
            Unlocked,
            Single,
            Progressive
        }
        
        public enum SeperateOption
        {
            Disabled,
            Unlocked,
            Single,
            Progressive,
            Seperate
        }

        public class Single : ItemOption
        {
            public SingleOption Default = SingleOption.Single;
        }

        public class Progressive : ItemOption
        {
            public ProgressiveOption Default = ProgressiveOption.Progressive;
        }

        public class Seperate : ItemOption
        {
            public SeperateOption Default = SeperateOption.Seperate;
        }
    }
}