namespace ParaboxArchipelago.Generation
{
    public class Possess
    {
        public class Option : IRegistered, IOption<ItemDefinition.SeperateValues>
        {
            public bool Is(params ItemDefinition.SeperateValues[] values)
            {
                return true;
            }
        }
        
        public class Single : ItemDefinition.SingleItem<Option, ItemDefinition.SeperateValues>, IFallbackProvider<object>
        {
            public void Logic(Registry r)
            {
                var x = r.Get<Possess.Option>();
                var y = x.Is(ItemDefinition.SeperateValues.Disabled);

                var v = new Version("1.1.0");
                var x2 = v >= v;
            }
            
            public FallbackDictionary<object> Fallbacks { get; } = new()
            {
                ["1.0.0"] = () => ""
            };
        }

        public class Progressive : ItemDefinition.ProgressiveItem<Option, ItemDefinition.SeperateValues>
        {
            
        }

        public class Wall : ItemDefinition.SeperateItem<Option, ItemDefinition.SeperateValues>
        {
            
        }

        public class Box : ItemDefinition.SeperateItem<Option, ItemDefinition.SeperateValues>
        {
            
        }
    }
}