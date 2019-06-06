using System;
using System.Windows;

namespace CMusicPlayer.Internal.Types.EventArgs
{
    public class PlayerUpdateEventArgs : System.EventArgs
    {
        public double Position { get; }
        public double Duration { get; }

        public PlayerUpdateEventArgs(TimeSpan position, Duration duration)
        {
            Position = position.TotalSeconds;
            if (duration.HasTimeSpan) Duration = duration.TimeSpan.TotalSeconds;
            else Duration = 0;
        }
    }
}
