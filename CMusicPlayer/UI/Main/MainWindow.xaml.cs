using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using CMusicPlayer.Configuration;
using CMusicPlayer.UI.Music.CloudTracks;
using CMusicPlayer.UI.Music.LocalTracks;
using CMusicPlayer.UI.Music.Queue;
using CMusicPlayer.UI.Utility;

namespace CMusicPlayer.UI.Main
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    internal partial class MainWindow 
    {
        private readonly LocalTracksView localTracksView;
        private readonly CloudTracksView cloudTracksView;
        private readonly QueueView queueView;
        private readonly MainViewModel vm;
        private readonly Action logoutCallback;
        private bool mouseDownOnSlider;

        public MainWindow(LocalTracksView localTracksView, CloudTracksView cloudTracksView, QueueView queueView, MainViewModel vm, Action logoutCallback)
        {
            InitializeComponent();
            this.localTracksView = localTracksView;
            this.cloudTracksView = cloudTracksView;
            this.queueView = queueView;
            this.vm = vm;
            this.logoutCallback = logoutCallback;

            DataContext = vm;

            new ApplicationBarEventHandler(this, AppBar);

            MainFrame.Content = localTracksView;

        }

        private void OnLocalTracksClicked(object sender, RoutedEventArgs e)
            => MainFrame.Content = localTracksView;

        private void OnCloudTracksClicked(object sender, RoutedEventArgs e)
            => MainFrame.Content = cloudTracksView;

        private void OnQueueClicked(object sender, RoutedEventArgs e)
            => MainFrame.Content = queueView;

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
//            Properties.Settings.Default.UserId = null;
//            Properties.Settings.Default.JwtToken = null;
//            Properties.Settings.Default.Save();
            logoutCallback();
            Close();
            SettingsManager.CreateNewTable("0");
            SettingsManager.CreateNewTable("1");
            SettingsManager.Settings["0"]["zero"] = "0zero";
            SettingsManager.Settings["0"]["one"] = "0one";
            SettingsManager.Settings["1"]["one"] = "1one";
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);
//            vm?.StopCommand?.Execute(null);
            Application.Current.Shutdown(0);
        }

        private void OnVolumeChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            var norm = e.NewValue / VolumeSlider.Maximum;
            vm?.HandleVolumeChanged(norm);
            VolumeLabel?.SetValue(ContentProperty, $"{(int)(norm * 100)}");
        }


    }

}
