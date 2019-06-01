using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using CMusicPlayer.Internal.Types.EventArgs;
using CMusicPlayer.Media.Models;
using CMusicPlayer.UI.Music.ViewModelBases;
using CMusicPlayer.Util.Functional;

namespace CMusicPlayer.UI.Music.Shared
{
    internal class TracksView : SwitchingPage
    {
        private readonly TrackListControl trackListControl;
        private readonly AlbumListControl albumListControl;
        private readonly ArtistListControl artistListControl;

        public TracksView(TrackListControl trackListControl, AlbumListControl albumListControl, ArtistListControl artistListControl, TracksViewModel viewModel) 
            : base(trackListControl, albumListControl, artistListControl, viewModel)
        {
            this.trackListControl = trackListControl;
            this.albumListControl = albumListControl;
            this.artistListControl = artistListControl;
            albumListControl.ToTracksByAlbum += OnToTracksByAlbum;
            artistListControl.ToAlbumsByArtist += OnToAlbumsByArtist;
        }

        /**
         * Called when artist is clicked from artistsListControl
         */
        private void OnToAlbumsByArtist(object sender, ArtistEventArgs e)
        {
            Func<IArtist, Task<IEnumerable<IAlbum>>> f = ViewModel.GetAlbumsByArtist;
            albumListControl.GetAlbums = f.Partial(e.Artist);
            ViewFrame.NavigationService.Navigate(albumListControl);
        }

        /**
         * Called when an album is clicked from albumsListControl
         */
        private void OnToTracksByAlbum(object sender, AlbumEventArgs e)
        {
            Func<IAlbum, Task<IEnumerable<ITrack>>> f = ViewModel.GetTracksByAlbum;
            trackListControl.GetTracks = f.Partial(e.Album);
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
