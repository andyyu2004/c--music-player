using System.Collections.Generic;
using CMusicPlayer.CLI.Lexing;
using CMusicPlayer.CLI.Parsing.Nodes;
using JetBrains.Annotations;

namespace CMusicPlayer.CLI.Parsing
{
    internal class Parser
    {
        private readonly CommandLineTextInterface clti;
        private int i;
        private List<Token> tokens = new List<Token>();

        public Parser(CommandLineTextInterface clti)
        {
            this.clti = clti;
        }

        private bool AtEnd => i >= tokens.Count;

        public Cmd? Parse(List<Token> ts)
        {
            tokens = ts;
            try
            {
                return ParseCommand();
            }
            catch (ParseError e)
            {
                clti.WriteError(e.Message);
                return null;
            }
        }

        // cmd ::= name <flag>* (<argflag> <arg>)* <arg>?
        private Cmd ParseCommand()
        {
            i = 0;
            var name = Expect(TokenType.Argument, $"Command name expected at {tokens[i]}");
            var cmd = new Cmd(name);
            while (!AtEnd)
                switch (tokens[i].Type)
                {
                    case TokenType.Flag:
                        ParseFlag(cmd);
                        break;
                    case TokenType.ArgFlag:
                        ParseArgFlag(cmd);
                        break;
                    case TokenType.Argument:
                        ParseArg(cmd);
                        break;
                }

            return cmd;
        }

        private void ParseArg(Cmd cmd)
        {
            if (cmd.Arg != null) throw new ParseError("Command can only have one unnamed argument");
            cmd.Arg = tokens[i++];
        }

        private void ParseArgFlag(Cmd cmd)
        {
            if (++i >= tokens.Count || tokens[i].Type != TokenType.Argument)
                throw new ParseError($"Argument expected after '{tokens[i - 1].Lexeme}'");
            cmd.FlagArgs[tokens[i - 1]] = tokens[i++];
        }

        private void ParseFlag(Cmd cmd)
        {
            cmd.Flags.Add(tokens[i++]);
        }

//        private bool Match(params TokenType[] types)
//        {
//            if (types.All(t => t != tokens[i].Type)) return false;
//            i++;
//            return true;
//        }
//

        [AssertionMethod]
        private Token Expect(TokenType type, string message)
        {
            if (tokens[i].Type != type) throw new ParseError(message);
            return tokens[i++];
        }
    }
}