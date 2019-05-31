using CMusicPlayer.CLI.Parsing.Nodes;
using CMusicPlayer.Media.Playback;

namespace CMusicPlayer.CLI.Commands
{
    internal class PauseCommand : MediaCommand
    {
        public PauseCommand(CommandLineTextInterface clti, IMediaPlayerController mp) : base(clti, mp) { }

        public override string Help { get; } = "[Usage]: pause";

        protected override void Run(Cmd cmd)
        {
            if (Mp.CurrentTrack == null)
            {
                Clti.WriteLine("No track is currently playing");
                return;
            }
            Mp.Pause();
            Clti.WriteLine("Paused");
        }

    }
}
