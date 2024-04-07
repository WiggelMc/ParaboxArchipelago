using System;

namespace ParaboxArchipelago.Gen
{
    public interface IItemDefinition : IRegistered
    {
        public FallbackValue<string> Name { get; }
        public FallbackValue<int> ID { get; }
        public FallbackValue<Func<Registry, ItemType>> Type { get; }
    }
}