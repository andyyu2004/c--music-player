using CMusicPlayer.UI.Music.Shared;
using CMusicPlayer.UI.Music.ViewModelBases;

namespace CMusicPlayer.UI.Music.CloudTracks
{
    internal class CloudTracksView : TracksView
    {
        public CloudTracksView(TrackListControl trackListControl, AlbumListControl albumListControl, ArtistListControl artistListControl, TracksViewModel vm)
            : base(trackListControl, albumListControl, artistListControl, vm)
        {

        }
    }
}
