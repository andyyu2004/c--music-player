using CMusicPlayer.Data.Repositories;
using CMusicPlayer.Media.Playback;
using CMusicPlayer.UI.Music.ViewModelBases;
using CMusicPlayer.Util;

namespace CMusicPlayer.UI.Music.LocalTracks
{
    internal class LocalTracksViewModel : TracksViewModel
    {
        public LocalTracksViewModel(IMediaPlayerController mp, ITrackRepository repository) : base(mp, repository)
        {
        }

        public override string LibraryName { get; } = Constants.Local;
    }
}