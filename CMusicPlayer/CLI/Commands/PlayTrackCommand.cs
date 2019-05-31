using System;
using System.Linq;
using CMusicPlayer.CLI.Lexing;
using CMusicPlayer.CLI.Parsing.Nodes;
using CMusicPlayer.Data.Databases;
using CMusicPlayer.Media.Playback;

namespace CMusicPlayer.CLI.Commands
{
    internal class PlayTrackCommand : MediaCommand
    {
        private readonly Database db;
        private readonly Token artistFlag = new Token("&r", TokenType.ArgFlag);
        private readonly Token albumFlag = new Token("&l", TokenType.ArgFlag);

        public PlayTrackCommand(CommandLineTextInterface clti, IMediaPlayerController mp, Database db) : base(clti, mp)
        {
            this.db = db;
            HasAnonymousArg = true;
            DeclareArgs();
        }

        private void DeclareArgs()
        {
            Optional.Add(artistFlag);
            Optional.Add(albumFlag);
        }


        // Is there a way to have a Task/void override
        protected override async void Run(Cmd cmd)
        {
            // We know track has value from checks before
            var track = cmd.Arg.GetValueOrDefault().Lexeme;
            var album = cmd.FlagArgs.ContainsKey(albumFlag) ? cmd.FlagArgs[albumFlag].Lexeme : null;
            var artist = cmd.FlagArgs.ContainsKey(artistFlag) ? cmd.FlagArgs[artistFlag].Lexeme : null;

            Clti.WriteLine($"Finding: {track} {album} {artist}");
            var tracks = (await db.LoadTracks())
                .Where(x => artist?.Equals(x.Artist ?? "", StringComparison.OrdinalIgnoreCase) ?? true)
                .Where(x => album?.Equals(x.Album ?? "", StringComparison.OrdinalIgnoreCase) ?? true)
                .Where(x => track.Equals(x.Title ?? "", StringComparison.OrdinalIgnoreCase)).ToList();
            if (tracks.Count == 0)
                Clti.WriteError("No tracks found matching criterion");
            else if (tracks.Count > 1) Clti.WriteLine("Multiple tracks found, please narrow criterion");
            tracks.ForEach(Clti.WriteLine);
            if (tracks.Count == 1) Mp.SetTrack(tracks[0]);
        }


        public override string Help { get; } = "[Usage]: playt [&r <artist>] [&l <album>] <track>";

    }
}
