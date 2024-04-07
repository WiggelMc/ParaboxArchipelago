using System;
using System.Collections.Generic;
using System.Linq;

namespace ParaboxArchipelago.Gen
{
    public static class ListExtensions
    {
        public static bool TryGetNextLower<TK, TV>(this SortedList<TK, TV> list, TK key, out KeyValuePair<TK, TV>? result) where TK : IComparable<TK>
        {
            if (list.Count == 0 || key.CompareTo(list.Keys.First()) < 0)
            {
                result = null;
                return false;
            }

            var resultKey = list.Keys.Last(k => key.CompareTo(k) >= 0);
            result = new KeyValuePair<TK, TV>(resultKey, list[resultKey]);
            return true;
        }

        public static TV GetNextLowerValueOrDefault<TK, TV>(this SortedList<TK, TV> list, TK key, TV defaultValue) where TK : IComparable<TK>
        {
            return list.TryGetNextLower(key, out var result) ? result!.Value.Value : defaultValue;
        }
    }
}