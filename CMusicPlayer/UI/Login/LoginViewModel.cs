using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace CMusicPlayer.UI.Login
{
    public class LoginViewModel : INotifyPropertyChanged
    {

//        private readonly LoginRepository repository;
//
//        // Bound To View
//        public string Email { get; set; }
//
//        public string ApiEndpoint { get; set; } = Properties.Settings.Default.ApiEndpoint ?? string.Empty;
//
//        private string message;
//        public string Message
//        {
//            get => message;
//            set { message = value; OnPropertyChanged(nameof(Message)); }
//        }
//
//        public LoginViewModel(LoginRepository repository)
//        {
//            this.repository = repository;
//        }
//
//        /// <summary>
//        /// Returns boolean indicating success or failure of login attempt
//        /// </summary>
//        /// <param name="password"></param>
//        /// <returns></returns>
//        public async Task<bool> OnLoginClicked(string password) // Pass in password as it is not bindable
//        {
//
//            // Convert To Api To Check For Errors
//            var apiUrl = ApiEndpoint.ToUri();
//            if (apiUrl.IsError)
//            {
//                Message = "Bad Api Endpoint";
//                return false;
//            }
//            apiUrl.Bind(uri => Properties.Settings.Default.ApiEndpoint = uri.ToString());
//            var res = await repository.ExecuteLogin(Email, password);
//            Message = res.Match(e => e.Message, str => str);
//            return res.IsSuccess;
//        }
//
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        

    }
}