using CMusicPlayer.Media.Models;

namespace CMusicPlayer.Internal.Types.EventArgs
{
    public class ArtistEventArgs : System.EventArgs
    {
        public ArtistEventArgs(IArtist artist)
        {
            Artist = artist;
        }

        public IArtist Artist { get; }
    }
}