using System.Collections.Generic;
using CMusicPlayer.CLI.Commands;
using CMusicPlayer.CLI.Parsing.Nodes;

namespace CMusicPlayer.CLI.Interpreting
{
    internal class Interpreter
    {
        private readonly CommandLineTextInterface clti;
        private readonly Dictionary<string, CliCommand> map;

        public Interpreter(CommandLineTextInterface clti, Dictionary<string, CliCommand> map)
        {
            this.clti = clti;
            this.map = map;
        }

        public void Interpret(Cmd cmd)
        {
            try
            {
                if (!map.ContainsKey(cmd.Name.Lexeme))
                    throw new InterpretationError($"Command '{cmd.Name.Lexeme}' does not exist");
                Execute(map[cmd.Name.Lexeme], cmd);
            }
            catch (InterpretationError e)
            {
                clti.WriteError(e.Message);
            }
        }

        private void Execute(CliCommand cliCommand, Cmd cmd)
        {
            try
            {
                cliCommand.Execute(cmd);
            }
            catch (ArgumentError e)
            {
                clti.WriteError(e.Message);
                clti.WriteError(cliCommand.Help);
            }
        }
    }
}