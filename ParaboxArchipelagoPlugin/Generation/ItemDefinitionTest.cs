using static ParaboxArchipelago.Generation.ItemDefinition;

namespace ParaboxArchipelago.Generation
{
    public class ItemDefinitionTest
    {
        public class OptionA : IOption<SingleValues>, IRegistered
        {
            public bool Is(params SingleValues[] values) => true;
        }

        public class ItemA : Item
        {
        }

        public class LoaderA : ISingleItemLoader
        {
            public Item Single(Registry r) => r.Get<ItemA>();
            public IOption<SingleValues> Option(Registry r) => r.Get<OptionA>();
        }

        public class OptionB : IOption<SeperateValues>, IRegistered
        {
            public bool Is(params SeperateValues[] values) => true;
        }

        public class LoaderB : ISeperateItemLoader
        {
            public Item Single(Registry r) => r.Get<ItemA>();

            public ProgressiveItem Progressive(Registry r) => new(r.Get<ItemA>(), 3);
            public int Count => 10;

            public SeperateItem[] Seperate(Registry r) => new SeperateItem[]
            {
                new(r.Get<ItemA>(), 1),
                new(r.Get<ItemA>(), 2),
                new(r.Get<ItemA>(), 3),
            };

            public IOption<SeperateValues> Option(Registry r) => r.Get<OptionB>();
        }
    }
}