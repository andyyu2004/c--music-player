using System;
using System.Collections;
using System.Collections.Generic;

namespace CMusicPlayer.Util.Extensions
{
    public static class EnumerableExt
    {

        public static void ForEach<T>(this IEnumerable<T> xs, Action<T> f)
        {
            foreach (var x in xs)
            {
                f(x);
            }
        }
    }
}
