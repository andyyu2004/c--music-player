using CMusicPlayer.Media.Models;

namespace CMusicPlayer.Internal.Types.EventArgs
{
    public class ArtistEventArgs : System.EventArgs
    {
       public IArtist Artist { get; }

       public ArtistEventArgs(IArtist artist)
       {
           Artist = artist;
       }
    }
}
