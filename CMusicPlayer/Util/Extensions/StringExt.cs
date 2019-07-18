using System;
using CMusicPlayer.Internal.Types.Functional;

namespace CMusicPlayer.Util.Extensions
{
    public static class StringExt
    {
        public static bool IsEmpty(this string s)
        {
            return string.IsNullOrWhiteSpace(s);
        }

        public static bool IsValidUri(this string s)
        {
            return Uri.TryCreate(s, UriKind.Absolute, out _)
                   && (s.StartsWith("http://")
                       || s.StartsWith("https://"));
        }

        public static Try<Uri> ToUri(this string str)
        {
            try
            {
                return new Uri(str);
            }
            catch (Exception e)
            {
                return e;
            }
        }
    }
}