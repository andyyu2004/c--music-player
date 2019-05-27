using System.Windows;
using CMusicPlayer.DependencyInjection;
using CMusicPlayer.UI.Login;
using Ninject;

namespace CMusicPlayer
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void AppStart(object sender, StartupEventArgs e)
        {
            var kernel = new StandardKernel(new Bindings());
            new LoginWindow(kernel.Get<LoginViewModel>()).Show();
        }
    }
}
