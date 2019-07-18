using CMusicPlayer.Media.Playback;

namespace CMusicPlayer.CLI.Commands
{
    internal abstract class MediaCommand : CliCommand
    {
        protected MediaCommand(CommandLineTextInterface clti, IMediaPlayerController mp) : base(clti)
        {
            Mp = mp;
        }

        protected IMediaPlayerController Mp { get; }
    }
}