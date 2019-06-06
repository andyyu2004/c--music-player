using System;
using System.Windows.Controls;
using System.Windows.Input;
using CMusicPlayer.Internal.Types.Commands;

namespace CMusicPlayer.UI.Music.Shared
{
    /// <summary>
    /// Interaction logic for PlaybackControl.xaml
    /// </summary>
    internal partial class PlaybackControl
    {
        public ICommand ShuffleAllCommand { get; }
        public event EventHandler ShuffleAll;

        public ICommand PlayAllCommand { get; }
        public event EventHandler PlayAll;

        public PlaybackControl()
        {
            InitializeComponent();
            DataContext = this;
            ShuffleAllCommand = new Command(() => ShuffleAll?.Invoke(this, EventArgs.Empty));
            PlayAllCommand = new Command(() => PlayAll?.Invoke(this, EventArgs.Empty));
        }
    }
}
