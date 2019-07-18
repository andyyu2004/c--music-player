using CMusicPlayer.Data.Repositories;
using CMusicPlayer.Media.Playback;
using CMusicPlayer.UI.Music.ViewModelBases;
using CMusicPlayer.Util;

namespace CMusicPlayer.UI.Music.CloudTracks
{
    internal class CloudTracksViewModel : TracksViewModel
    {
        public CloudTracksViewModel(IMediaPlayerController mediaController, ITrackRepository repository) : base(
            mediaController, repository)
        {
        }

        public override string LibraryName { get; } = Constants.Cloud;
    }
}