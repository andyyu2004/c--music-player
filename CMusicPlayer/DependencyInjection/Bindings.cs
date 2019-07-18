using CMusicPlayer.Data.Databases;
using CMusicPlayer.Data.Files;
using CMusicPlayer.Data.Network;
using CMusicPlayer.Data.Repositories;
using CMusicPlayer.Media.Playback;
using CMusicPlayer.UI.Music.CloudTracks;
using CMusicPlayer.UI.Music.LocalTracks;
using CMusicPlayer.UI.Music.ViewModelBases;
using CMusicPlayer.Util;
using Ninject.Modules;

namespace CMusicPlayer.DependencyInjection
{
    public class Bindings : NinjectModule
    {
//        https://github.com/ninject/ninject/wiki/Contextual-Binding

        public override void Load()
        {
            Bind<IMediaPlayerController>().To<MediaPlayerController>().InSingletonScope();
            Bind<IDatabase>().To<Database>().InSingletonScope();
            Bind<FileManager>().ToSelf().InSingletonScope();
            Bind<IHttpService>().To<HttpService>();

            Bind<NetworkDataSource>().ToSelf().InSingletonScope();
            Bind<LoginRepository>().ToSelf().InSingletonScope();

            // Local Bindings
            Bind<LocalTracksView>().ToSelf().Named(Constants.Local);
            Bind<TracksViewModel>().To<LocalTracksViewModel>().WhenAnyAncestorNamed(Constants.Local).InSingletonScope();
            Bind<ITrackRepository>().To<LocalRepository>().WhenAnyAncestorNamed(Constants.Local).InSingletonScope();

            // Cloud Bindings
            Bind<CloudTracksView>().ToSelf().Named(Constants.Cloud);
            Bind<TracksViewModel>().To<CloudTracksViewModel>().WhenAnyAncestorNamed(Constants.Cloud).InSingletonScope();
            Bind<ITrackRepository>().To<CloudRepository>().WhenAnyAncestorNamed(Constants.Cloud).InSingletonScope();
        }
    }
}