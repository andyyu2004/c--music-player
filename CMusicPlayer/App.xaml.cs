using System.Windows;
using CMusicPlayer.Configuration;
using CMusicPlayer.DependencyInjection;
using CMusicPlayer.Media.Playback;
using CMusicPlayer.UI.Login;
using CMusicPlayer.UI.Main;
using Microsoft.Win32;
using Ninject;

/**
 * TODO
 * Allow opening files in this
 * Allow deletion and clear database
 * Reduce memory consumption somehow
 * Implement queue to album and artist
 * Implement play album functionality in album view
 * Order album view by track number
 * RepeatEnabled
 * Add the settings constants to the Settings class not Constants (makes much more sense)
 */
namespace CMusicPlayer
{
    /// <summary>
    ///     Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        private readonly StandardKernel kernel = new StandardKernel(new Bindings());

        private void AppStart(object sender, StartupEventArgs e)
        {
            StartApp();
            SystemEvents.PowerModeChanged += SystemEventsOnPowerModeChanged;
            Exit += OnExit;
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
            MainWindow = kernel.Get<MainWindow>();
            kernel.Get<LoginWindow>();
        }

        private static void OnExit(object sender, ExitEventArgs e)
        {
            Config.Save();
            Current.Shutdown(0);
        }
    }
}