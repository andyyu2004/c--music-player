using System;

namespace MusicPlayer.Internal.Types
{
    public class PlaybackStateChangedEventArgs : EventArgs
    {
        public PlaybackStateChangedEventArgs(bool isPlaying)
        {
            IsPlaying = isPlaying;
        }

        public bool IsPlaying { get; }
    }
}