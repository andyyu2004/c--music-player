using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using CMusicPlayer.CLI;
using CMusicPlayer.Data.Files;
using CMusicPlayer.Internal.Types.Commands;

namespace CMusicPlayer.UI.Main
{
    internal class MainViewModel : INotifyPropertyChanged
    {

        private string notificationMessage = string.Empty;

        public string NotificationMessage
        {
            get => notificationMessage;
            set { notificationMessage = value; OnPropertyChanged(nameof(NotificationMessage)); }
        }

        public ICommand OpenCliCommand { get; }

        public ICommand OpenFolderDialogCommand { get; }

        // ReSharper disable once SuggestBaseTypeForParameter
        public MainViewModel(CommandLineWindow cli, FileManager fm)
        {
            OpenCliCommand = new Command(cli.Show);
            OpenFolderDialogCommand = new AsyncCommand(() => fm.AddLocalFolder(s => { NotificationMessage = s; }));
        }


        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
