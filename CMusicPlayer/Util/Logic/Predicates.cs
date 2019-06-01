using System;
using System.Collections.Generic;
using System.Text;

namespace CMusicPlayer.Util.Logic
{
    public static class Predicates
    {
        public static Func<object, bool> True() => _ => true;
    }
}
