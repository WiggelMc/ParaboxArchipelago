using System;

namespace ParaboxArchipelago.Gen.Example
{
    public static class Possess
    {
        [Register]
        public class Single : IItemDefinition
        {
            public Lifetime Lifetime =>
                Lifetime
                    .Since(APWorldVersion.Of("1.0.0"))
                    .Removed(APWorldVersion.Of("1.4.2"));

            public FallbackValue<string> Name =>
                FallbackValue<string>
                    .Of("Possess")
                    .Since(APWorldVersion.Of("1.2.1"), "Possessing");

            public FallbackValue<int> ID => new(1);
            public FallbackValue<Func<Registry, ItemType>> Type => new(_ => ItemType.Progression);
        }

        [Register]
        public class Progressive : IItemDefinition
        {
            public Lifetime Lifetime => Lifetime.Since(APWorldVersion.Of("1.0.0"));
            public FallbackValue<string> Name => new("Progressive Possess");
            public FallbackValue<int> ID => new(2);
            public FallbackValue<Func<Registry, ItemType>> Type => new(_ => ItemType.Progression);
        }

        [Register]
        public class SeperateWall : IItemDefinition
        {
            public Lifetime Lifetime => Lifetime.Since(APWorldVersion.Of("1.0.0"));
            public FallbackValue<string> Name => new("Possess Wall");
            public FallbackValue<int> ID => new(3);
            public FallbackValue<Func<Registry, ItemType>> Type => new(_ => ItemType.Progression);
        }

        [Register]
        public class SeperateBox : IItemDefinition
        {
            public Lifetime Lifetime => Lifetime.Since(APWorldVersion.Of("1.0.0"));
            public FallbackValue<string> Name => new("Possess Box");
            public FallbackValue<int> ID => new(4);
            public FallbackValue<Func<Registry, ItemType>> Type => new(_ => ItemType.Progression);
        }

        [Register]
        public class Option : IOptionDefinition
        {
            public Lifetime Lifetime => Lifetime.Since(APWorldVersion.Of("1.0.0"));
        }

        [Register]
        public class Pool : IPoolDefinition
        {
            public Lifetime Lifetime => Lifetime.Since(APWorldVersion.Of("1.0.0"));

            public FallbackValue<Func<Registry, IPoolGenerator>> Generator =>
                new(r => new SeperateItemPoolGenerator(
                    r.Get<Single>(),
                    r.Get<Progressive>(),
                    new IItemDefinition[]
                    {
                        r.Get<SeperateBox>(),
                        r.Get<SeperateWall>()
                    },
                    r.Get<Option>()
                ));
        }
    }
}