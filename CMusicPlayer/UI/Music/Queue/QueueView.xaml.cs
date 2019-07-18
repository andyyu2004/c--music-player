using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;
using CMusicPlayer.Data.Network.Types;
using CMusicPlayer.Internal.Types.EventArgs;
using CMusicPlayer.Media.Models;

namespace CMusicPlayer.UI.Music.Queue
{
    /// <summary>
    ///     Interaction logic for QueueView.xaml
    /// </summary>
    internal partial class QueueView
    {
        private readonly QueueViewModel vm;
        private int? dragIndex;

        public event EventHandler<ArtistEventArgs> ToArtist;
        public event EventHandler<AlbumEventArgs> ToAlbum; 

        public QueueView(QueueViewModel vm)
        {
            InitializeComponent();
            this.vm = vm;
            DataContext = vm;
            vm.PropertyChanged += VmOnPropertyChanged;
        }


        private void VmOnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName != nameof(vm.CurrentIndex) || TracksDataGrid.Items.IsEmpty) return;
            TracksDataGrid.ScrollIntoView(TracksDataGrid.Items[vm.CurrentIndex]);
        }

        private static bool IsMouseOnTargetRow(Visual? target, Func<IInputElement, Point> pos)
        {
            if (target == null) return false;
            var posBounds = VisualTreeHelper.GetDescendantBounds(target);
            var mousePos = pos((IInputElement) target);
            return posBounds.Contains(mousePos);
        }

        private DataGridRow? GetDataGridRow(int index)
        {
            if (TracksDataGrid.ItemContainerGenerator.Status != GeneratorStatus.ContainersGenerated)
                return null;
            return (DataGridRow) TracksDataGrid.ItemContainerGenerator.ContainerFromIndex(index);
        }

        private int? GetDataGridCurrentRowIndex(Func<IInputElement, Point> pos)
        {
            int? currIndex = null;
            for (var i = 0; i < TracksDataGrid.Items.Count; i++)
            {
                var item = GetDataGridRow(i);
                if (IsMouseOnTargetRow(item, pos))
                    currIndex = i;
            }

            return currIndex;
        }

        private void OnPreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            dragIndex = GetDataGridCurrentRowIndex(e.GetPosition);
            if (dragIndex == null) return;
            TracksDataGrid.SelectedIndex = dragIndex.Value;
            if (!(TracksDataGrid.Items[dragIndex.Value] is ITrack selectedTrack)) return; // Safety Check
            // Cannot move currently playing track
            if (dragIndex == vm.CurrentIndex) return;
            if (DragDrop.DoDragDrop(TracksDataGrid, selectedTrack, DragDropEffects.Move) != DragDropEffects.None)
                TracksDataGrid.SelectedItem = selectedTrack;
        }

        private void OnDrop(object sender, DragEventArgs e)
        {
            var dropIndex = GetDataGridCurrentRowIndex(e.GetPosition);
            if (dropIndex == null || dropIndex == vm.CurrentIndex) return;
            // Change from swap to removing and adding
            if (dragIndex == null) return;
            var tempTrack = vm.Queue[dragIndex.Value];
            vm.Queue[dragIndex.Value] = vm.Queue[dropIndex.Value];
            vm.Queue[dropIndex.Value] = tempTrack;
        }

        private void SetTrack(ITrack selectedTrack, int? i)
        {
            if (i == null) return;
            // Error checking
            if (vm.Queue[i.Value] != selectedTrack)
                MessageBox.Show("Something wrong with queue index");
            vm.JumpToIndex(i.Value);
        }

        private void OnViewPropertiesClicked(object sender, RoutedEventArgs e)
        {
            if (TracksDataGrid.SelectedItem is ITrack track)
                vm.ViewProperties(track);
        }

        private void OnToAlbum(object sender, RoutedEventArgs e)
        {
            if (!(TracksDataGrid.SelectedItem is ITrack track)) return;
            var album = new AlbumModel(track.AlbumId) {Album = track.Album, Artist = track.Artist, Year = track.Year};
            ToAlbum?.Invoke(this, new AlbumEventArgs(album));
        }

        private void OnToArtist(object sender, RoutedEventArgs e)
        {
            if (!(TracksDataGrid.SelectedItem is ITrack track)) return;
            var artist = new ArtistModel(track.Artist)
            {
                Id = track.ArtistId
            };
            ToArtist?.Invoke(this, new ArtistEventArgs(artist));

        }

        private void OnLoadingRow(object sender, DataGridRowEventArgs e)
        {
            e.Row.Header = $"{e.Row.GetIndex()}: ";
        }

        private void OnPreviewMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (TracksDataGrid.SelectedItem is ITrack track)
                SetTrack(track, TracksDataGrid.SelectedIndex);
        }
    }
}