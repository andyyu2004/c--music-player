using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace MusicPlayer.Internal.Types
{
    public class PlayerUpdateEventArgs : EventArgs
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
