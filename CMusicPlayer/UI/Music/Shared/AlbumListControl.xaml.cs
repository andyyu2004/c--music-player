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
    ///     Interaction logic for AlbumListControl.xaml
    /// </summary>
    internal partial class AlbumListControl : ISearchable, IRefreshable
    {
        private Func<Task<IEnumerable<IAlbum>>> getAlbums;

        public AlbumListControl(TracksViewModel vm)
        {
            InitializeComponent();
            DataContext = this;
            getAlbums = vm.GetAlbums;
        }

        public Func<Task<IEnumerable<IAlbum>>> GetAlbums
        {
            get => getAlbums;
            set
            {
                getAlbums = value;
                Refresh();
            }
        }

        public ObservableCollection<IAlbum> AlbumList { get; } = new ObservableCollection<IAlbum>();

        public async void Refresh()
        {
            AlbumList.Refresh(await GetAlbums.Invoke());
        }

        public bool FocusSearchElement()
        {
            return SearchControl.FocusInput();
        }

        public event EventHandler<AlbumEventArgs> ToTracksByAlbum;

        private void OnDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (AlbumsListGrid.SelectedItem is IAlbum album)
                ToTracksByAlbum?.Invoke(this, new AlbumEventArgs(album));
        }

        private void SearchTextChanged(object sender, TextChangedEventArgs e)
        {
            AlbumsListGrid.ItemsSource = AlbumList.Where(x => x.Search(SearchControl.SearchBox.Text));
        }
    }
}