using System.ComponentModel;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using CMusicPlayer.CLI;
using CMusicPlayer.Data.Files;
using CMusicPlayer.Internal.Types.Commands;
using CMusicPlayer.Internal.Types.EventArgs;
using CMusicPlayer.Media.Playback;
using CMusicPlayer.UI.Properties;
using JetBrains.Annotations;
using MusicPlayer.Internal.Types;

namespace CMusicPlayer.UI.Main
{
    internal class MainViewModel : INotifyPropertyChanged
    {

        private readonly IMediaPlayerController controller;

        private string currentLibrary = "Nothing";
        public string CurrentLibrary
        {
            get => currentLibrary;
            set { currentLibrary = value; OnPropertyChanged(nameof(CurrentLibrary)); }
        }

        private string currentTrackTitle = "Nothing";
        public string CurrentTrackTitle
        {
            get => currentTrackTitle;
            private set { currentTrackTitle = value; OnPropertyChanged(nameof(CurrentTrackTitle)); }
        }

        private string currentTrackAlbumArtist = "";
        public string CurrentTrackAlbumArtist
        {
            get => currentTrackAlbumArtist;
            private set { currentTrackAlbumArtist = value; OnPropertyChanged(nameof(CurrentTrackAlbumArtist)); }
        }

        // Current Position Of Current Track
        // Integer values to prevent casting errors from string to double in format
        private int position;
        public int Position
        {
            get => position;
            set { position = value; OnPropertyChanged(nameof(Position)); }
        }

        // Remaining Duration Of Current Track (Bind Remaining Time Indicator)
        private int remainingDuration;
        public int RemainingDuration
        {
            get => remainingDuration;
            set { remainingDuration = value; OnPropertyChanged(nameof(RemainingDuration)); }
        }

        // Total Duration Of Current Track (To Bind Slider Max)
        private int totalDuration;
        public int TotalDuration
        {
            get => totalDuration;
            set { totalDuration = value; OnPropertyChanged(nameof(TotalDuration)); }
        }

        // Path For PlayPauseButton
        private string playPauseImagePath = "../../Images/play_icon.png";
        public string PlayPauseImagePath
        {
            get => playPauseImagePath;
            private set { playPauseImagePath = value; OnPropertyChanged(nameof(PlayPauseImagePath)); }
        }

        private string notificationMessage = "";
        public string NotificationMessage
        {
            get => notificationMessage;
            set { notificationMessage = value; OnPropertyChanged(nameof(NotificationMessage)); }
        }

        // Playback Commands
        public ICommand PlayPauseCommand { get; }
        public ICommand PlayPrevCommand { get; }
        public ICommand PlayNextCommand { get; }
        public ICommand StopCommand { get; }

        public ICommand OpenCliCommand { get; }
        public ICommand OpenFileDialogCommand { get; }
        public ICommand OpenFolderDialogCommand { get; }
        public ICommand OpenPreferencesCommand { get; }

        [SuppressMessage("ReSharper", "SuggestBaseTypeForParameter")]
        public MainViewModel(IMediaPlayerController controller, FileManager fm, CommandLineWindow cli)
        {
            this.controller = controller;

            PlayPrevCommand = new Command(controller.SkipToPrev);
            PlayPauseCommand = new Command(controller.TogglePlayPause);
            PlayNextCommand = new Command(controller.SkipToNext);
            StopCommand = new Command(controller.Stop);

            OpenCliCommand = new Command(cli.Show);
            OpenPreferencesCommand = new Command(() => new PreferencesPane().Show());

            OpenFileDialogCommand = new AsyncCommand(() => fm.AddLocalFiles(s => { NotificationMessage = s; }));
            OpenFolderDialogCommand = new AsyncCommand(() => fm.AddLocalFolder(s => { NotificationMessage = s; }));

            controller.CurrentTrackChanged += OnCurrentTrackChanged;
            controller.PositionUpdated += OnPositionUpdated;
            controller.PlaybackStateChanged += OnPlaybackStateChanged;
            controller.LibraryChanged += OnLibraryChanged;
        }

        private void OnLibraryChanged(object sender, StringEventArgs e) => CurrentLibrary = e.Str;

        private void OnPlaybackStateChanged(object sender, PlaybackStateChangedEventArgs e) =>
            PlayPauseImagePath = e.IsPlaying ? "../../Images/pause_icon.png" : "../../Images/play_icon.png";

        private void OnPositionUpdated(object sender, PlayerUpdateEventArgs e)
        {
            Position = (int)e.Position;
            TotalDuration = (int)e.Duration;
            RemainingDuration = TotalDuration - Position;
        }

        private void OnCurrentTrackChanged(object sender, TrackEventArgs e)
        {
            CurrentTrackTitle = e.Track.Title ?? "";
            CurrentTrackAlbumArtist = $"{e.Track.Album} - {e.Track.Artist}";
        }

        public void OnSliderMouseUp(double pos)
        {
            controller.SeekTo(pos);
            // Try and prevent over subscription
            controller.PositionUpdated -= OnPositionUpdated;
            controller.PositionUpdated += OnPositionUpdated;
        }

        // Stops the slider moving when mouse down
        public void OnSliderMouseDown() => controller.PositionUpdated -= OnPositionUpdated;

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null) => 
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        public void HandleVolumeChanged(double volume) => controller.Volume = volume;

    }
}
