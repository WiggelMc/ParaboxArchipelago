using JetBrains.Annotations;
using ParaboxArchipelago.Generation;

namespace ParaboxArchipelago.Gen
{
    public sealed class Lifetime
    {
        private APWorldVersion since;
        [CanBeNull] private APWorldVersion removed;

        private Lifetime(APWorldVersion since)
        {
            this.since = since;
        }
        
        private Lifetime(APWorldVersion since, APWorldVersion removed)
        {
            this.since = since;
        }
        

        public static Lifetime Since(APWorldVersion version) => new(version);

        public Lifetime Removed(APWorldVersion version)
        {
            removed = version;
            return this;
        }
        
        public bool IsActive(APWorldVersion version)
        {
            return version >= since && (removed == null || version < removed);
        } 
    }
}