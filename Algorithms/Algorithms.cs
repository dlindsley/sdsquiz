using System;
using System.Linq;

namespace DeveloperSample.Algorithms
{
    public static class Algorithms
    {
        public static int GetFactorial(int n)
        {
            if (n < 0)
                throw new ArgumentOutOfRangeException(nameof(n), "Argument must be non-negative");

            if (n == 0 || n == 1)
                return 1;

            return n * GetFactorial(n - 1);
        }

        public static string FormatSeparators(params string[] items)
        {
            if (items == null || items.Length == 0)
                return string.Empty;

            if (items.Length == 1)
                return items[0];

            // personally I'd have added an Oxford comma ...
            return string.Join(", ", items.Take(items.Length - 1)) + " and " + items.Last();
        }
    }
}