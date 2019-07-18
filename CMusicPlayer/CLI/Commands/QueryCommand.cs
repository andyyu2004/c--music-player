using System;
using System.Reflection;
using CMusicPlayer.CLI.Interpreting;
using CMusicPlayer.CLI.Lexing;
using CMusicPlayer.CLI.Parsing.Nodes;
using CMusicPlayer.Data.Databases;
using CMusicPlayer.Media.Models;
using CMusicPlayer.Util.Extensions;

namespace CMusicPlayer.CLI.Commands
{
    internal class QueryCommand : CliCommand
    {
        private readonly IDatabase db;
        private readonly Token typeToken = new Token("&t", TokenType.ArgFlag);

        internal QueryCommand(CommandLineTextInterface clti, IDatabase db) : base(clti)
        {
            this.db = db;
            Required.Add(typeToken);
            HasAnonymousArg = true;
        }

        public override string Help { get; } =
            "[Usage]: query -t <type> <query> where <type> \u2208 {artist|r, album|l, track|t}";

        protected override void Run(Cmd cmd)
        {
            Clti.WriteLine("Running query");
            var typeString = cmd.FlagArgs[typeToken].Lexeme;
            var type = TypeOf(typeString);
            var method = GetType().GetMethod("RunQuery", BindingFlags.Instance | BindingFlags.NonPublic);
            method?.MakeGenericMethod(type).Invoke(this, new object[] {cmd});
        }

        // ReSharper disable once UnusedMember.Local
        private async void RunQuery<T>(Cmd cmd)
        {
            var res = await db.ExecuteQueryAsync<T>(cmd.Arg?.Lexeme ?? "");
            res.Match(
                e => Clti.WriteError(e.Message),
                xs => xs.ForEach(x => Clti.WriteLine(x))
            );
        }

        // Currently the database is organized only by a single Track table
        private Type TypeOf(string t)
        {
            t = t.ToLower();
            switch (t)
            {
                case "album":
                case "l":
                    return typeof(AlbumModel);

                case "artist":
                case "r":
                    return typeof(ArtistModel);

                case "track":
                case "t":
                    return typeof(TrackModel);

                default: throw new InterpretationError($"Invalid type at '{t}'");
            }
        }
    }
}