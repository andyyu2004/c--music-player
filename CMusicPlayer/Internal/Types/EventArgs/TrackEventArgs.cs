using CMusicPlayer.Media.Models;

namespace CMusicPlayer.Internal.Types.EventArgs
{
    internal class TrackEventArgs : System.EventArgs
    {
        public TrackEventArgs(ITrack track, int? index = null)
        {
            Track = track;
            Index = index;
        }

        public ITrack Track { get; }
        public int? Index { get; }
    }
}