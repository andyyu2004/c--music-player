using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using CMusicPlayer.Internal.Types.EventArgs;
using CMusicPlayer.Media.Models;
using CMusicPlayer.UI.General;
using CMusicPlayer.UI.Music.ViewModelBases;
using CMusicPlayer.Util.Extensions;

namespace CMusicPlayer.UI.Music.Shared
{
    /// <summary>
    ///     Interaction logic for TrackListControl.xaml
    /// </summary>
    internal partial class TrackListControl : ISearchable
    {
        public event EventHandler<AlbumEventArgs> ToAlbum;
        public event EventHandler<ArtistEventArgs> ToArtist;

        private Func<Task<IEnumerable<ITrack>>> getTracks;
        public Func<Task<IEnumerable<ITrack>>> GetTracks
        {
            get => getTracks;
            set
            {
                getTracks = value;
                RefreshTracks();
            }
        }

        private readonly TracksViewModel vm;

        public ObservableCollection<ITrack> TrackList { get; } = new ObservableCollection<ITrack>();

        public TrackListControl(TracksViewModel vm)
        {
            InitializeComponent();
            DataContext = this;
            this.vm = vm;
            getTracks = vm.GetTracks;
            RefreshTracks();
            PlaybackControl.ShuffleAll += vm.ShuffleAll;
        }

        private async void RefreshTracks() => SetTrackList(await GetTracks());

        private void SetTrackList(IEnumerable<ITrack> tracks) => TrackList.Refresh(tracks);

        private void OnTrackDoubleClicked(object sender, MouseButtonEventArgs e)
        {
            if (TracksListGrid.SelectedItem is ITrack track)
                vm.SetTrack(track);
        }

        private void OnPlayNextClicked(object sender, RoutedEventArgs e)
        {
            if (TracksListGrid.SelectedItem is ITrack track)
                vm.PlayTrackNext(track);
        }

        private void OnAddToQueueClicked(object sender, RoutedEventArgs e)
        {
            if (TracksListGrid.SelectedItem is ITrack track)
                vm.AddTrackToQueue(track);
        }

        private void OnViewPropertiesClicked(object sender, RoutedEventArgs e)
        {
            if (TracksListGrid.SelectedItem is ITrack track)
                vm.ViewProperties(track);
        }

        private void OnToAlbum(object sender, RoutedEventArgs e)
        {
            if (!(TracksListGrid.SelectedItem is ITrack t)) return;
            ToAlbum?.Invoke(this, new AlbumEventArgs(new AlbumModel(t.AlbumId)
            {
                Album = t.Album,
                Artist = t.Artist,
                Year = t.Year,
            }));
        }

        private void OnToArtist(object sender, RoutedEventArgs e)
        {
            if (!(TracksListGrid.SelectedItem is ITrack t)) return;
            ToArtist?.Invoke(this, new ArtistEventArgs(new ArtistModel(t.ArtistId)
            {
                Artist = t.Artist,
            }));
        }

        private void SearchTextChanged(object sender, TextChangedEventArgs e) => TracksListGrid.ItemsSource = TrackList.Where(track => track.Search(SearchControl.SearchBox.Text));

        public bool FocusSearchElement() => SearchControl.FocusInput();


    }
}