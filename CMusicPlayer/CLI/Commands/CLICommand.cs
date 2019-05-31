using System.Collections.Generic;
using System.Linq;
using CMusicPlayer.CLI.Lexing;
using CMusicPlayer.CLI.Parsing.Nodes;

namespace CMusicPlayer.CLI.Commands
{
    internal abstract class CliCommand
    {
        protected readonly CommandLineTextInterface Clti;
        public abstract string Help { get; }
        public HashSet<Token> Required { get; } = new HashSet<Token>();

        public HashSet<Token> Optional { get; } = new HashSet<Token>();

        public IEnumerable<Token> Args => Required.Concat(Optional);

        public bool HasAnonymousArg { get; protected set; } = false;

        protected CliCommand(CommandLineTextInterface clti)
        {
            Clti = clti;
        }

        protected abstract void Run(Cmd cmd);

        /**
         * Throws ArgumentError
         */
        public void Execute(Cmd cmd)
        {
            ValidateArgs(cmd);
            Run(cmd);
        }

        private void ValidateArgs(Cmd cmd)
        {
            if (HasAnonymousArg && cmd.Arg == null)
                throw new ArgumentError("Anonymous(unnamed) argument expected");
            if (!HasAnonymousArg && cmd.Arg != null) 
                throw new ArgumentError($"Unexpected anonymous(unnamed) argument '{cmd.Arg.Value.Lexeme}'");

            foreach (var x in Required)
                if (!cmd.Args.Contains(x)) throw new ArgumentError($"Argument '{x.Lexeme}' expected");

            foreach (var x in cmd.Args)
                if (!Args.Contains(x)) throw new ArgumentError($"Unexpected argument '{x.Lexeme}'");
        }
    }
}
