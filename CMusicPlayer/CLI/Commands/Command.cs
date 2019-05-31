using System;
using System.Collections.Generic;
using System.Text;
using CMusicPlayer.Data.Databases;
using CMusicPlayer.Media.Playback;

namespace CMusicPlayer.CLI.Commands
{
    internal class Command
    {
        public Dictionary<string, CliCommand> Map { get; }

        public Command(CommandLineTextInterface clti, IMediaPlayerController mp, Database db)
        {
            Map = new Dictionary<string, CliCommand>
            {
                { "play", new PlayCommand(clti, mp) },
                { "pause", new PauseCommand(clti, mp) },
                { "playt", new PlayTrackCommand(clti, mp, db) },
                { "next", new PlayPrevCommand(clti, mp) },
                { "query", new QueryCommand(clti, db) },
            };
        }
    }
}
