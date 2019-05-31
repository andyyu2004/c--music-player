using CMusicPlayer.Media.Playback;

namespace CMusicPlayer.CLI.Commands
{
    internal abstract class MediaCommand : CliCommand
    {
        protected readonly IMediaPlayerController Mp;

        protected MediaCommand(CommandLineTextInterface clti, IMediaPlayerController mp) : base(clti)
        {
            Mp = mp;
        }

    }
}
