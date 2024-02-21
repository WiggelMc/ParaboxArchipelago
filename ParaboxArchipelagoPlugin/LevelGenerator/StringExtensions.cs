using System.Linq;

namespace ParaboxArchipelago.LevelGenerator
{
    public static class StringExtensions
    {
        public static string PascalCaseToWordCase(this string input)
        {
            return string.Concat(input.Select((c, i) => i > 0 && char.IsUpper(c) ? " " + c : c.ToString()));
        }
    }
}