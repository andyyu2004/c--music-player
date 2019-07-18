using System;

namespace CMusicPlayer.Util.Logic
{
    public static class Predicates
    {
        public static Func<object, bool> True()
        {
            return _ => true;
        }
    }
}