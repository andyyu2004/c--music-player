using System;

namespace CMusicPlayer.Util.Functional
{
    public static class PartialApplication
    {
        // 1 -> 0 args
        public static Func<TU> Partial<T, TU>(this Func<T, TU> f, T x) => () => f(x);

        // Try partial apply one argument
        public static Func<TU> Partial<T, TU>(this Func<T, Func<TU>> f, T x) => f(x);

    }
}