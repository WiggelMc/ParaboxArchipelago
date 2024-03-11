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
            ProgressiveItemSpecifier Progressive(Registry r);
            int Count { get; }
            IOption<ProgressiveValues> Option(Registry r);
        }

        public class ProgressiveItemSpecifier
        {
            public ProgressiveItem Item;
            public int Count;

            public ProgressiveItemSpecifier(ProgressiveItem item, int count)
            {
                Item = item;
                Count = count;
            }
        }
        
        public class SeperateItemSpecifier
        {
            public Item Item;
            public int Stage;

            public SeperateItemSpecifier(Item item, int stage)
            {
                Item = item;
                Stage = stage;
            }
        }

        public interface ISeperateItemLoader
        {
            Item Single(Registry r);
            ProgressiveItemSpecifier Progressive(Registry r);
            SeperateItemSpecifier[] Seperate(Registry r);
            IOption<SeperateValues> Option(Registry r);
        }

        public interface IRequirement
        {
            
        }
        
        public abstract class Item : IRegistered, IRequirement
        {
            
        }

        public class ProgressiveRequirement : IRequirement
        {
            public ProgressiveItem Item;
            public int Stage;

            public ProgressiveRequirement(ProgressiveItem item, int stage)
            {
                Item = item;
                Stage = stage;
            }
        }
        
        public abstract class ProgressiveItem : Item
        {
            public ProgressiveRequirement Require(int stage) => new(this, stage);
        }
    }
}