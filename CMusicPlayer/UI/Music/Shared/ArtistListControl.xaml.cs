using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
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
    ///     Interaction logic for ArtistListControl.xaml
    /// </summary>
    internal partial class ArtistListControl : ISearchable, IRefreshable
    {
        private Func<Task<IEnumerable<IArtist>>> getArtists;

        public ArtistListControl(TracksViewModel vm)
        {
            InitializeComponent();
            DataContext = this;
            getArtists = vm.GetArtists;
        }

        public Func<Task<IEnumerable<IArtist>>> GetArtists
        {
            get => getArtists;
            set
            {
                getArtists = value;
                Refresh();
            }
        }

        public ObservableCollection<IArtist> ArtistList { get; } = new ObservableCollection<IArtist>();

        public async void Refresh()
        {
            ArtistList.Refresh(await GetArtists());
        }

        public bool FocusSearchElement()
        {
            return SearchControl.FocusInput();
        }

        public event EventHandler<ArtistEventArgs> ToAlbumsByArtist;

        private void OnDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (ArtistsGrid.SelectedItem is IArtist artist)
                ToAlbumsByArtist?.Invoke(this, new ArtistEventArgs(artist));
        }

        private void SearchTextChanged(object sender, TextChangedEventArgs e)
        {
            ArtistsGrid.ItemsSource = ArtistList.Where(t => t.Search(SearchControl.SearchBox.Text));
        }
    }
}