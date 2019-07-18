using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Navigation;
using CMusicPlayer.Internal.Types.EventArgs;
using CMusicPlayer.Media.Models;
using CMusicPlayer.UI.General;
using CMusicPlayer.UI.Music.ViewModelBases;
using CMusicPlayer.Util.Functional;

namespace CMusicPlayer.UI.Music.Shared
{
    internal class TracksView : SwitchingPage, IRefreshable
    {
        private readonly AlbumListControl albumListControl;
        private readonly ArtistListControl artistListControl;
        private readonly TrackListControl trackListControl;

        public TracksView(TrackListControl trackListControl, AlbumListControl albumListControl,
            ArtistListControl artistListControl, TracksViewModel viewModel)
            : base(trackListControl, albumListControl, artistListControl, viewModel)
        {
            this.trackListControl = trackListControl;
            this.albumListControl = albumListControl;
            this.artistListControl = artistListControl;

            albumListControl.ToTracksByAlbum += OnToTracksByAlbum;
            artistListControl.ToAlbumsByArtist += OnToAlbumsByArtist;
            trackListControl.ToAlbum += OnToTracksByAlbum;
            trackListControl.ToArtist += OnToAlbumsByArtist;

            Refresh();
        }

        public void Refresh() => CurrentControl.Refresh();

        /**
         * Called when artist is clicked from artistsListControl
         */
        private void OnToAlbumsByArtist(object sender, ArtistEventArgs e) => ToAlbumsByArtist(e.Artist);

        public void ToAlbumsByArtist(IArtist artist)
        {
            Func<IArtist, Task<IEnumerable<IAlbum>>> f = ViewModel.GetAlbumsByArtist;
            albumListControl.GetAlbums = f.Partial(artist);
            ViewFrame.NavigationService.Navigate(albumListControl);
            ViewFrame.NavigationUIVisibility = NavigationUIVisibility.Hidden;
        }

        /**
         * Called when an album is clicked from albumsListControl
         */
        private void OnToTracksByAlbum(object sender, AlbumEventArgs e) => ToTracksByAlbum(e.Album);

        public void ToTracksByAlbum(IAlbum album)
        {
            Func<IAlbum, Task<IEnumerable<ITrack>>> f = ViewModel.GetTracksByAlbum;
            trackListControl.GetTracks = f.Partial(album);
            ViewFrame.NavigationService.Navigate(trackListControl);
        }

        /**
         * Refresh the lists on click
         */
        protected override void OnToTracks(object sender, RoutedEventArgs e)
        {
            trackListControl.GetTracks = ViewModel.GetTracks;
            base.OnToTracks(sender, e);
        }

        protected override void OnToAlbums(object sender, RoutedEventArgs e)
        {
            albumListControl.GetAlbums = ViewModel.GetAlbums;
            base.OnToAlbums(sender, e);
        }

        protected override void OnToArtists(object sender, RoutedEventArgs e)
        {
            artistListControl.GetArtists = ViewModel.GetArtists;
            base.OnToArtists(sender, e);
        }
    }
}