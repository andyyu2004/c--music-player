using System.Windows;
using System.Windows.Input;
using CMusicPlayer.UI.Utility;

//using MusicPlayer.Internal.Types;
//using MusicPlayer.Util;

namespace CMusicPlayer.UI.Login
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow // Inherits from Window
    {

//        private readonly LoginViewModel vm;

        // Considered Logged In If There Is JwtToken
//        public bool IsLoggedIn => !string.IsNullOrEmpty(Properties.Settings.Default.JwtToken)
//                                  && !string.IsNullOrEmpty(Properties.Settings.Default.ApiEndpoint)
//                                  && !string.IsNullOrEmpty(Properties.Settings.Default.UserId);

//        public ICommand SubmitCommand { get; }

        public LoginWindow(LoginViewModel loginViewModel)
        {
            InitializeComponent();
            
//            vm = loginViewModel;
//            DataContext = vm;

            new ApplicationBarEventHandler(this, Bar);

            // For Key Bindings
//            SubmitCommand = new Command(() => OnSubmitLoginRequest(this, null));
        }

        private void OnSubmitLoginRequest(object sender, RoutedEventArgs e)
        {
            // As Extension Method Will Blow Up On Null 
//            if (string.IsNullOrWhiteSpace(EmailTextInput.Text) || string.IsNullOrWhiteSpace(PasswordTextInput.Password))
//            {
//                MessageLabel.Content = "Please Enter Email And Password";
//                return;
//            }
//            if (!await viewModel.OnLoginClicked(PasswordTextInput.Password)) return;
//            ToMainWindow();
        }

        private void ToMainWindow()
        {
            Close();
            Application.Current.MainWindow?.Show();
        }

        private void UseOffline(object sender, RoutedEventArgs e)
        {
            ToMainWindow();
        }
    }


}
