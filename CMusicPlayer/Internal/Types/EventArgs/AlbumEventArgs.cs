using CMusicPlayer.Media.Models;

namespace CMusicPlayer.Internal.Types.EventArgs
{
    public class AlbumEventArgs : System.EventArgs
    {
        public IAlbum Album { get; set; }

        public AlbumEventArgs(IAlbum album)
        {
            Album = album;
        }
    }
}
