using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
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
using CMusicPlayer.Util.Functional;

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
//            PlaybackControl.ShuffleAll += vm.ShuffleAll;
        }

        private async void RefreshTracks() => SetTrackList(await GetTracks());

        private void SetTrackList(IEnumerable<ITrack> tracks) => TrackList.Refresh(tracks);

        private void OnTrackDoubleClicked(object sender, MouseButtonEventArgs e)
        {
            if (TracksListGrid.SelectedItem is ITrack track)
                vm.SetTrack(track);
        }

        private void SearchTextChanged(object sender, TextChangedEventArgs e) => TracksListGrid.ItemsSource = TrackList.Where(track => track.Search(SearchControl.SearchBox.Text));

        public bool FocusSearchElement() => SearchControl.FocusInput();
    }
}