using CMusicPlayer.CLI.Parsing.Nodes;
using CMusicPlayer.Media.Playback;

namespace CMusicPlayer.CLI.Commands
{
    internal class PlayPrevCommand : MediaCommand
    {
        public PlayPrevCommand(CommandLineTextInterface clti, IMediaPlayerController mp) : base(clti, mp)
        {
        }

        public override string Help { get; } = "[Usage]: prev";

        protected override void Run(Cmd cmd)
        {
            if (Mp.CurrentTrack == null)
            {
                Clti.WriteError("There is no currently playing track");
                return;
            }

            if (Mp.Queue.IndexOf(Mp.CurrentTrack) == 0)
            {
                Clti.WriteError("There is no previous track in queue");
                return;
            }

            Mp.SkipToPrev();
            Clti.WriteLine($"Playing previous track {Mp.CurrentTrack.Title}");
        }
    }
}