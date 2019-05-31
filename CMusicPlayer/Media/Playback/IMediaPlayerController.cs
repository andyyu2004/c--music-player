using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using CMusicPlayer.Internal.Types.EventArgs;
using CMusicPlayer.Media.Models;
using CMusicPlayer.UI.Music.ViewModelBases;
using MusicPlayer.Internal.Types;

namespace CMusicPlayer.Media.Playback
{
    internal interface IMediaPlayerController
    {
        event EventHandler<PlayerUpdateEventArgs> PositionUpdated;
        event EventHandler<PlaybackStateChangedEventArgs> PlaybackStateChanged;
        event EventHandler<TrackEventArgs> CurrentTrackChanged;
        event EventHandler<StringEventArgs> LibraryChanged;

        bool IsPlaying { get; }
        List<ITrack> Tracks { get; }
        void SetTrackList(TracksViewModel sender, List<ITrack> xs);
        void SetTrack(ITrack track);
        double Volume { get; set; }
        void TogglePlayPause();
        void Play();
        void Pause();
        void Stop();
        void PlayTrackNext(ITrack track);
        void AddTrackToQueue(ITrack track);
        void SkipToPrev();
        void SkipToNext();
        void SeekTo(double pos);
        void ShuffleAll();

        ObservableCollection<ITrack> Queue { get; }
        int QueueIndex { get; }
        ITrack? CurrentTrack { get; }
        void JumpToIndex(int index);

    }
}
