using System.Windows;
using System.Windows.Forms;
using CMusicPlayer.Internal.Types.EventArgs;
using CMusicPlayer.UI.Music.ViewModelBases;

namespace CMusicPlayer.UI.Music.Shared
{
    /// <summary>
    /// Interaction logic for SwitchingPage.xaml
    /// </summary>
    internal abstract partial class SwitchingPage
    {
        protected TracksViewModel ViewModel { get; }
        protected TrackListControl TrackListControl { get; }
        protected AlbumListControl AlbumListControl { get; }
        protected ArtistListControl ArtistListControl { get; }

        protected SwitchingPage(TrackListControl trackListControl, AlbumListControl albumListControl, ArtistListControl artistListControl, TracksViewModel viewModel)
        {
            InitializeComponent();
            TrackListControl = trackListControl;
            AlbumListControl = albumListControl;
            ArtistListControl = artistListControl;
            ViewModel = viewModel;

            ViewFrame.Content = trackListControl;

            albumListControl.ToTracksByAlbum += OnToTracksByAlbum;
        }

        private void OnToTracksByAlbum(object sender, AlbumEventArgs e)
        {
            ViewFrame.NavigationService.Navigate(TrackListControl);
        }

        private void OnBack(object sender, RoutedEventArgs e) => ViewFrame.NavigationService.GoBack();

        protected virtual void OnToArtists(object sender, RoutedEventArgs e) => ViewFrame.Content = ArtistListControl;

        protected virtual void OnToAlbums(object sender, RoutedEventArgs e) => ViewFrame.Content = AlbumListControl;

        protected virtual void OnToGenres(object sender, RoutedEventArgs e)
        {
            
        }

        protected virtual void OnToTracks(object sender, RoutedEventArgs e) => ViewFrame.Content = TrackListControl;
    }
}
