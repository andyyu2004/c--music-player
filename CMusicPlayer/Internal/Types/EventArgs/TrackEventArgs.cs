using CMusicPlayer.Media.Models;

namespace CMusicPlayer.Internal.Types.EventArgs
{
    internal class TrackEventArgs : System.EventArgs
    {
        public ITrack Track { get; }
        public int? Index { get; }

        public TrackEventArgs(ITrack track, int? index = null)
        {
            Track = track;
            Index = index;
        }
    }
}
