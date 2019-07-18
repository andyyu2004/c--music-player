using System.Collections.Generic;
using System.Diagnostics;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using CMusicPlayer.Configuration;
using CMusicPlayer.Internal.Types.EventArgs;
using CMusicPlayer.Media.Models;
using CMusicPlayer.UI.Music.CloudTracks;
using CMusicPlayer.UI.Music.LocalTracks;
using CMusicPlayer.UI.Music.Queue;
using CMusicPlayer.UI.Music.Shared;
using CMusicPlayer.UI.Utility;
using CMusicPlayer.Util;

namespace CMusicPlayer.UI.Main
{
    /// <summary>
    ///     Interaction logic for MainWindow.xaml
    /// </summary>
    internal partial class MainWindow
    {
        private readonly CloudTracksView cloudTracksView;
        private readonly LocalTracksView localTracksView;
        private readonly QueueView queueView;
        private readonly MainViewModel vm;
        private bool mouseDownOnSlider;
        private readonly Dictionary<string, TracksView> viewMap;


        public MainWindow(LocalTracksView localTracksView, CloudTracksView cloudTracksView, QueueView queueView,
            MainViewModel vm)
        {
            InitializeComponent();
            this.localTracksView = localTracksView;
            this.cloudTracksView = cloudTracksView;
            this.queueView = queueView;
            queueView.ToArtist += QueueViewOnToArtist;
            queueView.ToAlbum += QueueViewOnToAlbum;
            this.vm = vm;

            viewMap = new Dictionary<string, TracksView>()
            {
                {Constants.Local, localTracksView },
                {Constants.Cloud, cloudTracksView }
            };
            
            DataContext = vm;

            new ApplicationBarEventHandler(this, AppBar, Application.Current.Shutdown);

            MainFrame.Content = localTracksView;
        }

        private void QueueViewOnToAlbum(object sender, AlbumEventArgs e)
        {
            var view = viewMap[vm.CurrentLibrary];
            view.ToTracksByAlbum(e.Album);
            MainFrame.Content = view;
        }

        private void QueueViewOnToArtist(object sender, ArtistEventArgs e)
        {
            var view = viewMap[vm.CurrentLibrary];
            view.ToAlbumsByArtist(e.Artist);
            MainFrame.Content = view;
        }

        private void OnLocalTracksClicked(object sender, RoutedEventArgs e)
        {
            localTracksView.Refresh();
            MainFrame.Content = localTracksView;
        }

        private void OnCloudTracksClicked(object sender, RoutedEventArgs e)
        {
            cloudTracksView.Refresh();
            MainFrame.Content = cloudTracksView;
        }

        private void OnQueueClicked(object sender, RoutedEventArgs e) => MainFrame.Content = queueView;

        private void OnSliderMouseUp(object sender, MouseButtonEventArgs e)
        {
            mouseDownOnSlider = false;
            vm?.OnSliderMouseUp(Slider.Value);
        }

        // This event is on the stackpanel containing the slider and previewmousedown doesn't work for some reason on slider directly
        private void OnSliderMouseDown(object sender, MouseButtonEventArgs e)
        {
            var rect = VisualTreeHelper.GetDescendantBounds(Slider);
            if (!rect.Contains(e.GetPosition(Slider))) return;
            mouseDownOnSlider = true;
            vm.OnSliderMouseDown();
        }

        private void OnSliderMouseMove(object sender, MouseEventArgs e)
        {
            if (!mouseDownOnSlider) return;
            var width = Slider.ActualWidth;
            var norm = e.GetPosition(Slider).X / width;
            Slider.Value = norm * Slider.Maximum;
        }


        private void HandleLogout(object sender, RoutedEventArgs e)
        {
            Config.Default(Config.Authentication);
            Config.Save();
            Process.Start(@"./Scripts/start.bat");
            Application.Current.Shutdown(0);
        }

        private void OnFocusSearch()
        {
//            if (MainFrame.Content is TracksView t) t.FocusSearchText();
        }

        private void OnVolumeChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            var norm = e.NewValue / VolumeSlider.Maximum;
            vm?.HandleVolumeChanged(norm);
            VolumeLabel?.SetValue(ContentProperty, $"{(int) (norm * 100)}");
        }

    }
}