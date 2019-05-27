using System;

namespace CMusicPlayer.UI.ApplicationBar
{
    public interface IApplicationBar
    {
        event EventHandler BarMouseDown;
        event EventHandler MinimizeClicked;
        event EventHandler MaximizeClicked;
        event EventHandler CloseClicked;
    }
}
