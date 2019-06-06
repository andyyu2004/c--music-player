using CMusicPlayer.Data.Repositories;
using CMusicPlayer.Media.Playback;
using CMusicPlayer.UI.Music.ViewModelBases;

namespace CMusicPlayer.UI.Music.LocalTracks
{
    internal class LocalTracksViewModel : TracksViewModel
    {

        public override string LibraryName { get; } = "Local";

        public LocalTracksViewModel(IMediaPlayerController mp, ITrackRepository repository) : base(mp, repository)
        {
        }

    }

}