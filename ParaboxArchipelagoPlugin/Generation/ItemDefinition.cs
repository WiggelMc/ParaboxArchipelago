namespace ParaboxArchipelago.Generation
{
    public class ItemDefinition
    {
        public enum SingleValues
        {
            Disabled,
            Unlocked,
            Single,
            Progressive,
            Seperate
        }
        
        public enum ProgressiveValues
        {
            Disabled,
            Unlocked,
            Single,
            Progressive,
            Seperate
        }
        
        public enum SeperateValues
        {
            Disabled,
            Unlocked,
            Single,
            Progressive,
            Seperate
        }

        public class Item : IRegistered
        {
            
        }
        
        public class OptionItem<TOption, TEnum> : Item 
            where TOption : IOption<TEnum>
            where TEnum : System.Enum
        {
            
        }

        public class SingleItem<TOption, TEnum> : OptionItem<TOption, TEnum>
            where TOption : IOption<TEnum>
            where TEnum : System.Enum
        {
            
        }
        
        public class ProgressiveItem<TOption, TEnum> : OptionItem<TOption, TEnum>
            where TOption : IOption<TEnum>
            where TEnum : System.Enum
        {
            
        }
        
        public class SeperateItem<TOption, TEnum> : OptionItem<TOption, TEnum>
            where TOption : IOption<TEnum>
            where TEnum : System.Enum
        {
            
        }
    }
}