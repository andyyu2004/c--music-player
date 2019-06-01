using CMusicPlayer.Media.Playback;

namespace CMusicPlayer.CLI.Commands
{
    internal abstract class MediaCommand : CliCommand
    {
        protected IMediaPlayerController Mp { get; }

        protected MediaCommand(CommandLineTextInterface clti, IMediaPlayerController mp) : base(clti)
        {
            Mp = mp;
        }

    }
}
