using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace CMusicPlayer.Util.Extensions
{
    public static class ObservableCollectionExt
    {
        public static void Refresh<T>(this ObservableCollection<T> xs, IEnumerable<T> ys)
        {
            xs.Clear();
            ys.ForEach(xs.Add);
        }
    }
}