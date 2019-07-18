using System.Collections.Generic;
using System.Linq;
using CMusicPlayer.CLI.Lexing;

namespace CMusicPlayer.CLI.Parsing.Nodes
{
    internal class Cmd
    {
        public Cmd(Token name)
        {
            Name = name;
        }

        public Token Name { get; }
        public List<Token> Flags { get; } = new List<Token>();

        // Alternate implementation
        public Dictionary<Token, Token> FlagArgs { get; } = new Dictionary<Token, Token>();
        public Token? Arg { get; set; }


        public List<Token> Args => Flags.Concat(FlagArgs.Keys).ToList();

        public override string ToString()
        {
            return base.ToString();
        }

//            => $"{Name} {Flags.Repr()} {FlagArgs} {Arg}";
    }
}