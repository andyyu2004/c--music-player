using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;

namespace CMusicPlayer.Util.Extensions
{
    internal static class ListExt
    {
        public static T? RandomElement<T>(this IList<T> xs) where T : class
        {
            var gen = new Random();
            if (xs.Count == 0) return default;
            var randInt = gen.Next(xs.Count - 1);
            return xs.ElementAt(randInt);
        }


    }
}
