using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;
using CMusicPlayer.Media.Models;

namespace CMusicPlayer.UI.Music.Queue
{
    /// <summary>
    /// Interaction logic for QueueView.xaml
    /// </summary>
    internal partial class QueueView
    {
        private readonly QueueViewModel vm;
        private int? dragIndex;

        public QueueView(QueueViewModel vm)
        {
            InitializeComponent();
            this.vm = vm;
            DataContext = vm;
        }

        private static bool IsMouseOnTargetRow(Visual? target, Func<IInputElement, Point> pos)
        {
            if (target == null) return false;
            var posBounds = VisualTreeHelper.GetDescendantBounds(target);
            var mousePos = pos((IInputElement)target);
            return posBounds.Contains(mousePos);
        }

        private DataGridRow? GetDataGridRow(int index)
        {
            if (TracksListView.ItemContainerGenerator.Status != GeneratorStatus.ContainersGenerated)
                return null;
            return (DataGridRow)TracksListView.ItemContainerGenerator.ContainerFromIndex(index);
        }

        private int? GetDataGridCurrentRowIndex(Func<IInputElement, Point> pos)
        {
            int? currIndex = null;
            for (var i = 0; i < TracksListView.Items.Count; i++)
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
            TracksListView.SelectedIndex = dragIndex.Value;
            if (!(TracksListView.Items[dragIndex.Value] is ITrack selectedTrack)) return; // Safety Check
            if (DragDrop.DoDragDrop(TracksListView, selectedTrack, DragDropEffects.Move) != DragDropEffects.None)
                TracksListView.SelectedItem = selectedTrack;

        }

        private void OnDrop(object sender, DragEventArgs e)
        {
            var dropIndex = GetDataGridCurrentRowIndex(e.GetPosition);
            // Treat Drag and Drop on same cell as attempted double click
            if (dropIndex == dragIndex && TracksListView.SelectedItem is ITrack selectedTrack) SetTrack(selectedTrack, dragIndex);
            if (dropIndex == null || dropIndex == TracksListView.Items.Count - 1) return;
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
            if (TracksListView.SelectedItem is ITrack track)
                vm.ViewProperties(track);
        }

        private void OnToAlbum(object sender, RoutedEventArgs e)
        {
            
        }

        private void OnToArtist(object sender, RoutedEventArgs e)
        {
        }
    }
}
