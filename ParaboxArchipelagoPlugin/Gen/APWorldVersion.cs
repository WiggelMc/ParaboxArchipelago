using ParaboxArchipelago.Generation;

namespace ParaboxArchipelago.Gen
{
    public sealed class APWorldVersion : Version<APWorldVersion>
    {
        public APWorldVersion(string identifier) : base(identifier) { }
        public static APWorldVersion Of(string identifier) => new(identifier);
    }
}