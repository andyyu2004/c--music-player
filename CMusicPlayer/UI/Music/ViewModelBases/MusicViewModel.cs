using CMusicPlayer.Media.Models;
using CMusicPlayer.Media.Playback;
using CMusicPlayer.UI.Properties;

namespace CMusicPlayer.UI.Music.ViewModelBases
{
    /**
     * Shared logic for track views as well queue
     */
    internal abstract class MusicViewModel
    {
        protected IMediaPlayerController Mp { get; }

        protected MusicViewModel(IMediaPlayerController mp) => Mp = mp;

        public void SetTrack(ITrack track) => Mp.SetTrack(track);

        public void ViewProperties(ITrack track)
        {
            var popup = new PropertiesWindow(track);
            popup.Show();
        }
    }
}
