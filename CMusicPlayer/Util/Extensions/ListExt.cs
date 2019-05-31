using System;
using System.Collections.Generic;
using System.Linq;

namespace CMusicPlayer.Util.Extensions
{
    internal static class ListExt
    {
        public static void Print<T>(this IList<T> xs) => Console.WriteLine($@"[{string.Join(",", xs)}]");

        public static T RandomElement<T>(this IList<T> xs)
        {
            var gen = new Random();
            if (xs.Count == 0) throw new ArgumentException("Empty list provided to RandomElement");
            var randInt = gen.Next(xs.Count - 1);
            return xs.ElementAt(randInt);
        }

    }
}
