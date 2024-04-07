using System;

namespace ParaboxArchipelago.Gen
{
    public interface IPoolDefinition : IRegistered
    {
        public FallbackValue<Func<Registry, IPoolGenerator>> Generator { get; }
    }
}