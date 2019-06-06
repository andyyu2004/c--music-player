using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using CMusicPlayer.Configuration;
using CMusicPlayer.Data.Repositories;
using CMusicPlayer.Util;
using CMusicPlayer.Util.Extensions;
using static CMusicPlayer.Util.Constants;

namespace CMusicPlayer.UI.Login
{
    internal class LoginViewModel : INotifyPropertyChanged
    {
        private readonly LoginRepository repository;

        // Bound To View
        // Todo Add Validation
        public string Email { get; set; } = "";

        private string? apiEndpoint = Config.Settings[Authentication][Constants.ApiEndpoint];
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
                Config.Settings[Authentication][Constants.ApiEndpoint] = value;
                Message = string.Empty;
            }
        } 

        private string message = "";
        public string Message
        {
            get => message;
            set { message = value; OnPropertyChanged(nameof(Message)); }
        }

        public LoginViewModel(LoginRepository repository) => this.repository = repository;

        /// <summary>
        /// Returns boolean indicating success or failure of login attempt
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

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        

    }
}