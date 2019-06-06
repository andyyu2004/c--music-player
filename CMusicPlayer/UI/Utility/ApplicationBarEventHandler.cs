using System;
using System.Windows;
using CMusicPlayer.UI.ApplicationBar;

namespace CMusicPlayer.UI.Utility
{
    public class ApplicationBarEventHandler
    {
        private readonly Window w;
        private readonly Action shutdownAction;

        public ApplicationBarEventHandler(Window w, IApplicationBar bar, Action? shutdownAction = null)
        {
            this.w = w;
            shutdownAction ??= w.Close;
            this.shutdownAction = shutdownAction;
            bar.BarMouseDown += OnAppBarMouseDown;
            bar.MaximizeClicked += OnMaximizeClicked;
            bar.CloseClicked += OnCloseClicked;
            bar.MinimizeClicked += OnMinimizeClicked;
        }

        public void OnCloseClicked(object sender, EventArgs e)
            => shutdownAction.Invoke();

        public void OnMaximizeClicked(object sender, EventArgs e)
        {
            w.WindowState = w.WindowState == WindowState.Normal
                ? WindowState.Maximized
                : WindowState.Normal;
        }

        public void OnMinimizeClicked(object sender, EventArgs e) => w.WindowState = WindowState.Minimized;

        public void OnAppBarMouseDown(object sender, EventArgs e) => w.DragMove();
    }
}
