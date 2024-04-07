using ParaboxArchipelago.Generation;

namespace ParaboxArchipelago.Gen
{
    public sealed class ClientVersion : Version<ClientVersion>
    {
        public ClientVersion(string identifier) : base(identifier) { }
        public static ClientVersion Of(string identifier) => new(identifier);
    }
}