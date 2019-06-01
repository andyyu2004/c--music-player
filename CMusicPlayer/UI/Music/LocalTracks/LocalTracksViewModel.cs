using CMusicPlayer.Data.Files;
using CMusicPlayer.Data.Repositories;
using CMusicPlayer.Media.Models;
using CMusicPlayer.Media.Playback;
using CMusicPlayer.UI.Music.ViewModelBases;
using CMusicPlayer.Util.Logic;

namespace CMusicPlayer.UI.Music.LocalTracks
{
    internal class LocalTracksViewModel : TracksViewModel
    {

        // ReSharper disable once SuggestBaseTypeForParameter
        public LocalTracksViewModel(IMediaPlayerController mp, LocalRepository repository, FileManager fm) : base(mp, repository)
        {
//            fm.FilesUploaded += (sender, args) => RefreshAll();
        }

    }

}