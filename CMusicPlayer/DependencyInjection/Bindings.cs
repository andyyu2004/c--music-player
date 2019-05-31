using System.Data;
using CMusicPlayer.CLI;
using CMusicPlayer.Data.Databases;
using CMusicPlayer.Media.Playback;
using Ninject.Modules;

namespace CMusicPlayer.DependencyInjection
{
    public class Bindings : NinjectModule
    {
        public override void Load()
        {
            Bind<IMediaPlayerController>().To<MediaPlayerContainer>().InSingletonScope();
            Bind<IDatabase>().To<Database>().InSingletonScope();

        }
    }
}
