using System.Collections.Generic;

namespace CMusicPlayer.Util.Extensions
{
    public static class CollectionExt
    {
        public static void Refresh<T>(this ICollection<T> xs, IEnumerable<T> ys)
        {
            xs.Clear();
            ys.ForEach(xs.Add);
        }
    }
}
