using System;

namespace CMusicPlayer.Util.Functional
{
    public static class PartialApplication
    {
        public static Action Partial<T>(this Action<T> f, T x)
        {
            return () => f(x);
        }

        // 1 -> 0 args
        public static Func<TU> Partial<T, TU>(this Func<T, TU> f, T x)
        {
            return () => f(x);
        }

        public static Func<TU, TW> Partial<T, TU, TW>(this Func<T, TU, TW> f, T x)
        {
            return y => f(x, y);
        }
    }
}