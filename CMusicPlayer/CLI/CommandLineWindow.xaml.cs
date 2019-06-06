
using System.ComponentModel;
using CMusicPlayer.CLI.Commands;
using CMusicPlayer.CLI.Interpreting;
using CMusicPlayer.CLI.Lexing;
using CMusicPlayer.CLI.Parsing;
using CMusicPlayer.Data.Databases;
using CMusicPlayer.Internal.Types.EventArgs;
using CMusicPlayer.Media.Playback;

namespace CMusicPlayer.CLI
{
    /// <summary>
    /// Interaction logic for CommandLineWindow.xaml
    /// </summary>
    internal partial class CommandLineWindow
    {
        private readonly Lexer lexer;
        private readonly Parser parser;
        private readonly Interpreter interpreter;

        public CommandLineWindow(IMediaPlayerController mp, Database db)
        {
            InitializeComponent();
            lexer = new Lexer(Clti);
            parser = new Parser(Clti);
            interpreter = new Interpreter(Clti, new Command(Clti, mp, db).Map);
            Clti.ReadLine += OnReadLine;
        }

        private void OnReadLine(object sender, StringEventArgs e) => ExecuteCommand(e.Str);

        private void ExecuteCommand(string command)
        {
            var tokens = lexer.Lex(command);
            var cmd = parser.Parse(tokens);
            if (cmd == null) return;
            interpreter.Interpret(cmd);
        }

        private void OnClosing(object sender, CancelEventArgs e)
        {
            e.Cancel = true;
            Hide();
        }
    }
}
