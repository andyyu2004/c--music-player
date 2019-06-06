using CMusicPlayer.Data.Files;
using CMusicPlayer.UI.Music.Shared;
using CMusicPlayer.UI.Music.ViewModelBases;

namespace CMusicPlayer.UI.Music.LocalTracks
{
    internal class LocalTracksView : TracksView
    {
        public LocalTracksView(TrackListControl trackListControl, AlbumListControl albumListControl, ArtistListControl artistListControl, TracksViewModel viewModel, FileManager fm) 
            : base(trackListControl, albumListControl, artistListControl, viewModel) =>
            fm.FilesUploaded += (sender, args) => RefreshAll();

        /**
         * Refreshes the data on all controls
         */
        public void RefreshAll()
        {
            TrackListControl.GetTracks = ViewModel.GetTracks;
            AlbumListControl.GetAlbums = ViewModel.GetAlbums;
            ArtistListControl.GetArtists = ViewModel.GetArtists;
        }
    }
}
