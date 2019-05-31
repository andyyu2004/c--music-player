using System.Windows;
using CMusicPlayer.Configuration;
using CMusicPlayer.DependencyInjection;
using CMusicPlayer.Media.Playback;
using CMusicPlayer.UI.Login;
using CMusicPlayer.UI.Main;
using CMusicPlayer.UI.Music.CloudTracks;
using CMusicPlayer.UI.Music.LocalTracks;
using CMusicPlayer.UI.Music.Queue;
using Microsoft.Win32;
using Ninject;

namespace CMusicPlayer
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        private readonly StandardKernel kernel = new StandardKernel(new Bindings());

        private void AppStart(object sender, StartupEventArgs e)
        {
            StartApp();
            SystemEvents.PowerModeChanged += SystemEventsOnPowerModeChanged;
        }

        private void SystemEventsOnPowerModeChanged(object sender, PowerModeChangedEventArgs e)
        {
            switch (e.Mode)
            {
                case PowerModes.Suspend:
                    kernel.Get<IMediaPlayerController>().Pause();
                    break;
            }
        }

        private void StartApp()
        {
            var loginWindow = new LoginWindow(kernel.Get<LoginViewModel>());
            MainWindow = new MainWindow(
                kernel.Get<LocalTracksView>(),
                kernel.Get<CloudTracksView>(),
                kernel.Get<QueueView>(),
                kernel.Get<MainViewModel>(),
                StartApp
            );
            MainWindow.Show();
            loginWindow.Close();
        }

        private void OnExit(object sender, ExitEventArgs e)
        {
            SettingsManager.Save();
        }


    }
}
