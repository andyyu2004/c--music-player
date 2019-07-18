using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Media;
using CMusicPlayer.Internal.Types.EventArgs;
using CMusicPlayer.Media.Models;
using CMusicPlayer.Statistics;
using CMusicPlayer.UI.Music.ViewModelBases;
using CMusicPlayer.Util.Extensions;
using JetBrains.Annotations;
using MusicPlayer.Internal.Types;

namespace CMusicPlayer.Media.Playback
{
    internal class MediaPlayerController : IMediaPlayerController, INotifyPropertyChanged
    {
        private readonly MediaPlayer mp = new MediaPlayer();
        private readonly StatisticsManager sm;

        // Timer For Updating Now Playing Progress
        private readonly Timer timer = new Timer(500);

        private ITrack? currentTrack;

        private bool isPlaying; // false is c# default value

        private int queueIndex = -1;

        private bool tempIsPlaying;

        private List<ITrack> tracks = new List<ITrack>();

        // Where the generated queue should begin (default 0)
        private int userQueueIndex;

        // 0 <= volume <= 1; default = 0.5
        private double volume = 1;
        

        public MediaPlayerController(StatisticsManager sm)
        {
            this.sm = sm;
            timer.Elapsed += OnTimeElapsed;
            timer.Start();
            mp.MediaEnded += (sender, args) =>
            {
                if (RepeatEnabled) RepeatTrack();
                else SkipToNext();
            };
            mp.Volume = 1;
        }

        public static int QueueLength { get; } = 10;

        private bool repeatEnabled;
        public bool RepeatEnabled
        {
            get => repeatEnabled;
            set
            {
                if (value == repeatEnabled) return;
                repeatEnabled = value;
                OnPropertyChanged(nameof(RepeatEnabled));
            }
        }

        // Only replays on automatic completion
        private bool shuffleEnabled = true;
        public bool ShuffleEnabled
        {
            get => shuffleEnabled;
            set
            {
                shuffleEnabled = value;
                OnPropertyChanged(nameof(ShuffleEnabled));
            }
        }

        public string CurrentLibrary { get; private set; } = "";

        public ObservableCollection<ITrack> Queue { get; } = new ObservableCollection<ITrack>();

        public List<ITrack> Tracks
        {
            get => tracks;
            set
            {
                tracks = value;
                LibraryChanged?.Invoke(this, new StringEventArgs(CurrentLibrary));
            }
        }

        public bool IsPlaying
        {
            get => isPlaying;
            private set
            {
                isPlaying = value;
                OnPlaybackStateChanged(value);
            }
        }

        public int QueueIndex
        {
            get => queueIndex;
            private set
            {
                if (value < 0) return;
                if (value > userQueueIndex) userQueueIndex = value;
                queueIndex = value;
                CurrentTrack = Queue[value];
                FillQueue();
            }
        }

        public ITrack? CurrentTrack
        {
            get => currentTrack;
            private set
            {
                currentTrack = value;
                OnCurrentTrackChanged(value);
            }
        }

        public double Volume
        {
            get => volume;
            set
            {
                volume = value;
                mp.Volume = value;
            }
        }

        // Events
        public event EventHandler<PlayerUpdateEventArgs> PositionUpdated;
        public event EventHandler<PlaybackStateChangedEventArgs> PlaybackStateChanged;
        public event EventHandler<StringEventArgs> LibraryChanged;


        // Events
        public event EventHandler<TrackEventArgs> CurrentTrackChanged;

        public void PlayTrackNext(ITrack track)
        {
            Queue.Insert(QueueIndex + 1, track);
            userQueueIndex++;
        }

        public void SkipToPrev()
        {
            if (mp.Position.TotalSeconds > 2)
                mp.Position = TimeSpan.FromMilliseconds(0);
            else QueueIndex--;
        }

        public void SkipToNext()
        {
            if (QueueIndex >= Queue.Count - 1) return;
            QueueIndex++;
        }

        public void ShuffleAll()
        {
            if (Tracks.Count == 0)
            {
                MessageBox.Show("Empty Track List");
                return;
            }

            ClearQueue();

            for (var i = 0; i < QueueLength; i++)
            {
                var track = Tracks.RandomElement();
                if (track != null) Queue.AddCopy(track);
            }

            QueueIndex = 0;

            Play();
        }

        public void JumpToIndex(int index) => QueueIndex = index;

        public void AddTrackToQueue(ITrack track)
        {
            try
            {
                Queue.Insert(++userQueueIndex, track);
            }
            catch (ArgumentOutOfRangeException)
            {
                Queue.AddCopy(track);
                userQueueIndex = Queue.Count - 1;
            }
        }

        public void SetTrackList(TracksViewModel sender, List<ITrack> xs)
        {
            CurrentLibrary = sender.LibraryName;
            Tracks = xs;
        }

        public void SetTrack(ITrack track)
        {
            Queue.Insert(QueueIndex + 1, track);
            // Keep increment separate don't want it to affect user queue
            QueueIndex++;
            LoadCurrentTrack();
            Play();
        }

        public void TogglePlayPause()
        {
            if (IsPlaying) Pause();
            else Play();
        }

        public void SeekTo(double pos) => 
            mp.Position = TimeSpan.FromSeconds(pos);

        public void Play()
        {
            if (mp.Source == null)
            {
                if (Queue.Count > 0) SetTrack(Queue[0]);
                return;
            }

            IsPlaying = true;
            mp.Play();
        }

        public void Pause()
        {
            if (!mp.CanPause) return;
            IsPlaying = false;
            mp.Pause();
        }

        public void Stop()
        {
            Pause();
            mp.Stop();
        }

        protected virtual void OnPlaybackStateChanged(bool state) => 
            PlaybackStateChanged?.Invoke(this, new PlaybackStateChangedEventArgs(state));


        private void OnTimeElapsed(object sender, ElapsedEventArgs e)
        {
            try
            {
                mp.Dispatcher.Invoke(() =>
                    PositionUpdated?.Invoke(this, new PlayerUpdateEventArgs(mp.Position, mp.NaturalDuration)));
            }
            catch (TaskCanceledException exception)
            {
                Console.WriteLine(exception);
            }
        }

        protected virtual void OnCurrentTrackChanged(ITrack? track)
        {
            if (track == null) return;
            LoadCurrentTrack();
            CurrentTrackChanged?.Invoke(this, new TrackEventArgs(track));
        }

        public void FillQueue()
        {
            var repeats = QueueLength - (Queue.Count - QueueIndex);
            var trackIndex = Tracks.FindIndex(x => x.TrackId == CurrentTrack?.TrackId);
            for (var i = 0; i < repeats; i++)
            {
                var track = ShuffleEnabled ? Tracks.RandomElement() : Tracks[trackIndex + i];
                if (track != null) Queue.AddCopy(track);
            }
        }

        private void ClearQueue()
        {
            Queue.Clear();
            userQueueIndex = -1;
        }

        private void LoadCurrentTrack()
        {
            SavePlaybackState(); // This is required, otherwise will not automatically start playing again automatically
            mp.Open(new Uri(currentTrack?.Path));
            RestorePlaybackState();
        }

        private void RepeatTrack() => mp.Position = TimeSpan.Zero;

        private void SavePlaybackState() => tempIsPlaying = IsPlaying;

        private void RestorePlaybackState()
        {
            if (tempIsPlaying) Play();
            else Pause();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null) => 
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}