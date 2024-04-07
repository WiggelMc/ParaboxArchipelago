using System;
using System.Collections.Generic;

namespace ParaboxArchipelago.Gen
{
    public class FallbackValue<T>
    {
        private readonly T initial;
        private readonly Lazy<SortedList<APWorldVersion, T>> fallbacks = new();
        
        public FallbackValue(T initial)
        {
            this.initial = initial;
        }

        public static FallbackValue<T> Of(T initial) => new(initial);

        public FallbackValue<T> Since(APWorldVersion version, T value)
        {
            fallbacks.Value.Add(version, value);
            return this;
        }

        public T Get(APWorldVersion version)
        {
            if (!fallbacks.IsValueCreated)
                return initial;

            return fallbacks.Value.GetNextLowerValueOrDefault(version, initial);
        }
    }
}