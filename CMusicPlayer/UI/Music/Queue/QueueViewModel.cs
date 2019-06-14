using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using CMusicPlayer.Media.Models;
using CMusicPlayer.Media.Playback;
using CMusicPlayer.UI.Music.ViewModelBases;
using JetBrains.Annotations;

namespace CMusicPlayer.UI.Music.Queue
{
    internal class QueueViewModel : MusicViewModel, INotifyPropertyChanged
    {

        public ObservableCollection<ITrack> Queue => Mp.Queue;

        private int currentIndex;
        public int CurrentIndex
        {
            get => currentIndex;
            private set
            {
                currentIndex = value;
                OnPropertyChanged(nameof(CurrentIndex));
            }
        }

        public QueueViewModel(IMediaPlayerController mp) : base(mp) => 
            mp.CurrentTrackChanged += (sender, args) => CurrentIndex = mp.QueueIndex;

        public void JumpToIndex(int index) => Mp.JumpToIndex(index);

        public event PropertyChangedEventHandler PropertyChanged;
        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null) => 
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
