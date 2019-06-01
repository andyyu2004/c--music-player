using System.IO;
using CMusicPlayer.Data.Databases;
using CMusicPlayer.Data.Files;
using CMusicPlayer.Media.Playback;
using CMusicPlayer.UI.Music.LocalTracks;
using CMusicPlayer.UI.Music.ViewModelBases;
using Ninject.Modules;

namespace CMusicPlayer.DependencyInjection
{
    public class Bindings : NinjectModule
    {
//        https://github.com/ninject/ninject/wiki/Contextual-Binding
        public override void Load()
        {
            Bind<IMediaPlayerController>().To<MediaPlayerContainer>().InSingletonScope();
            Bind<IDatabase>().To<Database>().InSingletonScope();
            Bind<FileManager>().ToSelf().InSingletonScope();

            // Local Bindings
            Bind<LocalTracksView>().ToSelf().Named("local");
            Bind<TracksViewModel>().To<LocalTracksViewModel>().WhenAnyAncestorNamed("local").InSingletonScope();
        }
    }
}
