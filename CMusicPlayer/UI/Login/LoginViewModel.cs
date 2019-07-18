using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using CMusicPlayer.Configuration;
using CMusicPlayer.Data.Repositories;
using CMusicPlayer.Util.Extensions;

namespace CMusicPlayer.UI.Login
{
    internal class LoginViewModel : INotifyPropertyChanged
    {
        private readonly LoginRepository repository;

        private string? apiEndpoint = Config.Settings[Config.Authentication][Config.ApiEndpoint];

        private string message = "";

        public LoginViewModel(LoginRepository repository)
        {
            this.repository = repository;
        }

        // Bound To View
        // Todo Add Validation
        public string Email { get; set; } = "";

        public string? ApiEndpoint
        {
            get => apiEndpoint;
            set
            {
                apiEndpoint = value;
                if (!value?.IsValidUri() ?? true)
                {
                    Message = "Invalid URI";
                    return;
                }

                // Only valid urls are saved into config
                Config.Settings[Config.Authentication][Config.ApiEndpoint] = value;
                Message = string.Empty;
            }
        }

        public string Message
        {
            get => message;
            set
            {
                message = value;
                OnPropertyChanged(nameof(Message));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        ///     Returns boolean indicating success or failure of login attempt
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        public async Task<bool> OnLoginClicked(string password) // Pass in password as it is not bindable
        {
            if (!ApiEndpoint?.IsValidUri() ?? true) return false;
            var res = await repository.ExecuteLogin(Email, password);
            Message = res.Match(e => e.Message, str => str);
            return res.IsSuccess;
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}