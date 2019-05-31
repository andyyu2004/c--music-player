using System;

namespace CMusicPlayer.UI.Music.LocalTracks
{
    /// <summary>
    /// Interaction logic for LocalTracksView.xaml
    /// </summary>
    internal partial class LocalTracksView
    {
        private readonly LocalTracksViewModel vm;

        public LocalTracksView(LocalTracksViewModel vm)
        {
            InitializeComponent();
            this.vm = vm;
            DataContext = vm;


        }


    }
}
