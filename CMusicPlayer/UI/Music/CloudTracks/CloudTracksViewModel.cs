using CMusicPlayer.Data.Repositories;
using CMusicPlayer.Media.Playback;
using CMusicPlayer.UI.Music.ViewModelBases;

namespace CMusicPlayer.UI.Music.CloudTracks
{
    internal class CloudTracksViewModel : TracksViewModel
    {
        public override string LibraryName { get; } = "Cloud";

        public CloudTracksViewModel(IMediaPlayerController mediaController, ITrackRepository repository) : base(mediaController, repository)
        {
        }
    }
}
