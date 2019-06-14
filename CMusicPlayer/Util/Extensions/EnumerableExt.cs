using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace CMusicPlayer.Util.Extensions
{
    public static class EnumerableExt
    {
        public static void ForEach<T>(this IEnumerable<T> xs, Action<T> f)
        {
            foreach (var x in xs) f(x);
        }

        public static void Print<T>(this IEnumerable<T> xs) => Debug.WriteLine($"[{string.Join(",", xs)}]");

        public static IEnumerable<T> FilterAll<T>(this IEnumerable<T> xs, IEnumerable<Func<T, bool>> filters) => 
            xs.Where(x => filters.All(f => f(x)));
    }
}
