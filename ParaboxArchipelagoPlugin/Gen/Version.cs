using System;
using System.Linq;

namespace ParaboxArchipelago.Gen
{
    public abstract class Version<T> : IComparable<Version<T>> where T : Version<T>
    {
        private readonly int[] parts;

        protected Version(string identifier)
        {
            parts = identifier.Split('.')
                .Select(int.Parse)
                .ToArray();
        }

        public int CompareTo(Version<T> other)
        {
            var length = Math.Max(this.parts.Length, other.parts.Length);
            for (var i = 0; i < length; i++)
            {
                var thisValue = this.parts.Length > i ? this.parts[i] : 0;
                var otherValue = other.parts.Length > i ? other.parts[i] : 0;
                var difference = thisValue - otherValue;
                if (difference != 0)
                    return difference;
            }
            
            return 0;
        }

        private bool Equals(Version<T> other) => CompareTo(other) == 0;

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((T)obj);
        }

        public override int GetHashCode()
        {
            var hash = 0;
            var stored = 1471157789;
            
            foreach (var part in parts)
            {
                hash += part * stored;
                stored += hash * 1340721049;
            }

            return hash;
        }
        
        public static bool operator<(Version<T> a, Version<T> b) => a.CompareTo(b) < 0;
        public static bool operator>(Version<T> a, Version<T> b) => a.CompareTo(b) > 0;
        public static bool operator<=(Version<T> a, Version<T> b) => a.CompareTo(b) <= 0;
        public static bool operator>=(Version<T> a, Version<T> b) => a.CompareTo(b) >= 0;
    }
}