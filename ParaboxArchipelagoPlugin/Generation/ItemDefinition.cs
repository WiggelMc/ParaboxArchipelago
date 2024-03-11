using System;
using System.Collections.Generic;

namespace ParaboxArchipelago.Generation
{
    public static class ItemDefinition
    {
        public enum SingleValues
        {
            Disabled,
            Unlocked,
            Single
        }

        public enum ProgressiveValues
        {
            Disabled,
            Unlocked,
            Single,
            Progressive
        }

        public enum SeperateValues
        {
            Disabled,
            Unlocked,
            Single,
            Progressive,
            Seperate
        }

        public interface ISingleItemLoader
        {
            Item Single(Registry r);
            IOption<SingleValues> Option(Registry r);
        }

        public interface IProgressiveItemLoader
        {
            Item Single(Registry r);
            ProgressiveItem Progressive(Registry r);
            int Count { get; }
            IOption<ProgressiveValues> Option(Registry r);
        }

        public class ProgressiveItem
        {
            public Item Item;
            public int Count;

            public ProgressiveItem(Item item, int count)
            {
                Item = item;
                Count = count;
            }
        }
        
        public class SeperateItem
        {
            public Item Item;
            public int Stage;

            public SeperateItem(Item item, int stage)
            {
                Item = item;
                Stage = stage;
            }
        }

        public interface ISeperateItemLoader
        {
            Item Single(Registry r);
            ProgressiveItem Progressive(Registry r);
            int Count { get; }
            SeperateItem[] Seperate(Registry r);
            IOption<SeperateValues> Option(Registry r);
        }

        public abstract class Item : IRegistered
        {
            
        }
    }
}