using System;

namespace TaskCommander
{
    public static class Strings
    {
        public static bool Matches(this string source, string compare)
        {
            return String.Equals(source, compare, StringComparison.OrdinalIgnoreCase);
        }
        public static bool MatchesAny(this string source, string [] compareList)
        {
            foreach (string compare in compareList)
                if (source.Matches(compare))
                    return true;
            return false;
        }
    }
}
