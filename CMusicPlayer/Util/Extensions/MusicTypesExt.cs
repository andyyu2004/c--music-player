using CMusicPlayer.Media.Models;

namespace CMusicPlayer.Util.Extensions
{
    public static class MusicTypesExt
    {
        internal static bool Search(this ITrack x, string str)
        {
            str = str.ToUpper();
            return x.Artist != null && x.Artist.ToUpper().Contains(str)
                   || x.Album != null && x.Album.ToUpper().Contains(str)
                   || x.Title != null && x.Title.ToUpper().Contains(str)
                   || x.Genre != null && x.Genre.ToUpper().Contains(str);
        }

        public static bool Search(this IAlbum x, string s)
        {
            s = s.ToUpper();
            return x.Artist != null && x.Artist.ToUpper().Contains(s)
                   || x.Album != null && x.Album.ToUpper().Contains(s)
                   || x.Genre != null && x.Genre.ToUpper().Contains(s);
        }

        public static bool Search(this IArtist x, string s)
            => x.Artist != null && x.Artist.ToUpper().Contains(s.ToUpper());
    }
}
