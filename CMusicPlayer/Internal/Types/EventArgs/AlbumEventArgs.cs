using CMusicPlayer.Media.Models;

namespace CMusicPlayer.Internal.Types.EventArgs
{
    public class AlbumEventArgs : System.EventArgs
    {
        public AlbumEventArgs(IAlbum album)
        {
            Album = album;
        }

        public IAlbum Album { get; set; }
    }
}