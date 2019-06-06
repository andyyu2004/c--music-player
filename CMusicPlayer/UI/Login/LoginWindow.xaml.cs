using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using CMusicPlayer.Configuration;
using CMusicPlayer.Internal.Types.Commands;
using CMusicPlayer.Internal.Types.DataStructures;
using CMusicPlayer.UI.Utility;
using CMusicPlayer.Util.Extensions;
using CMusicPlayer.Util.Functional;
using static CMusicPlayer.Util.Constants;

//using MusicPlayer.Internal.Types;
//using MusicPlayer.Util;

namespace CMusicPlayer.UI.Login
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    internal partial class LoginWindow
    {

        private readonly LoginViewModel vm;

        // Considered logged in if there exists token, api, and userid
        public bool IsLoggedIn => (!auth[JwtToken]?.IsEmpty() ?? false)
                                  && (!auth[ApiEndpoint]?.IsEmpty() ?? false)
                                  && (!auth[UserId]?.IsEmpty() ?? false);

        private readonly NDictionary<string, string> auth = Config.Settings[Authentication];

        public ICommand SubmitCommand { get; }

        public LoginWindow(LoginViewModel loginViewModel)
        {
            InitializeComponent();
            vm = loginViewModel;
            DataContext = vm;

            new ApplicationBarEventHandler(this, Bar, new Action<int>(Application.Current.Shutdown).Partial(0));

            Config.CreateNewTable(Authentication);
            
            SubmitCommand = new AsyncCommand(SubmitLoginRequest);

            if (!IsLoggedIn) Show();
            else ToMainWindow();
        }

        private async Task SubmitLoginRequest()
        {
            // As Extension Method Will Blow Up On Null 
            if (EmailTextInput.Text.IsEmpty() || PasswordTextInput.Password.IsEmpty())
            {
                MessageLabel.Content = "Please Enter Email And Password";
                return;
            }
            if (!await vm.OnLoginClicked(PasswordTextInput.Password)) return;
            ToMainWindow();
        }

        private void ToMainWindow()
        {
            Application.Current.MainWindow?.Show();
            Close();
        }

        private void UseOffline(object sender, RoutedEventArgs e) => ToMainWindow();
    }


}
