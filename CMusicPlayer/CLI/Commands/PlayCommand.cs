using CMusicPlayer.CLI.Parsing.Nodes;
using CMusicPlayer.Media.Playback;

namespace CMusicPlayer.CLI.Commands
{
    internal class PlayCommand : MediaCommand
    {
        public PlayCommand(CommandLineTextInterface clti, IMediaPlayerController mp) : base(clti, mp)
        {
        }

        public override string Help { get; } = "[Usage]: play";


        protected override void Run(Cmd cmd)
        {
            if (Mp.CurrentTrack == null)
            {
                Clti.WriteLine("No track is currently playing");
                return;
            }

            Clti.WriteLine($"Playing '{Mp.CurrentTrack.Title}'");
            Mp.Play();
        }
    }
}