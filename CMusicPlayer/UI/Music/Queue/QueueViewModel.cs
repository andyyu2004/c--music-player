using System.Collections.ObjectModel;
using CMusicPlayer.Media.Models;
using CMusicPlayer.Media.Playback;
using CMusicPlayer.UI.Music.ViewModelBases;

namespace CMusicPlayer.UI.Music.Queue
{
    internal class QueueViewModel : MusicViewModel
    {

        public ObservableCollection<ITrack> Queue => Mp.Queue;

        public QueueViewModel(IMediaPlayerController mp) : base(mp)
        {
            
        }

        public void JumpToIndex(int index) => Mp.JumpToIndex(index);
    }
}
