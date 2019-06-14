using System.Collections.Generic;
using CMusicPlayer.Internal.Interfaces;

namespace CMusicPlayer.Util.Extensions
{
    public static class CollectionExt
    {
        public static void Refresh<T>(this ICollection<T> xs, IEnumerable<T> ys)
        {
            xs.Clear();
            ys.ForEach(xs.Add);
        }

        internal static void AddCopy<T>(this ICollection<T> xs, T x) where T : IShallowCopyable =>
            xs.Add((T)x.ShallowCopy());
//        {
//            if (xs.Contains(x)) xs.Add((T) x.ShallowCopy());
//            else xs.Add(x);
//        }


    }
}
