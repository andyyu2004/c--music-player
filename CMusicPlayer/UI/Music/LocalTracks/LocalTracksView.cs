using CMusicPlayer.UI.Music.Shared;
using CMusicPlayer.UI.Music.ViewModelBases;

namespace CMusicPlayer.UI.Music.LocalTracks
{
    internal class LocalTracksView : TracksView
    {
        public LocalTracksView(TrackListControl trackListControl, AlbumListControl albumListControl, ArtistListControl artistListControl, TracksViewModel viewModel) 
            : base(trackListControl, albumListControl, artistListControl, viewModel)
        {
        }

    }
}
