using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using CMusicPlayer.CLI.Lines;
using CMusicPlayer.Internal.Types.EventArgs;

namespace CMusicPlayer.CLI
{
    /// <summary>
    ///     Interaction logic for CommandLineTextInterface.xaml
    /// </summary>
    public partial class CommandLineTextInterface
    {
        public static readonly DependencyProperty CommandListProperty =
            DependencyProperty.Register(nameof(CommandList), typeof(IEnumerable<string>),
                typeof(CommandLineTextInterface));

        private readonly List<string> history = new List<string>(); // Stack doesn't work as may need to go down again

        private Line currLine = new Line();
        private int historyIndex;

        public CommandLineTextInterface()
        {
            InitializeComponent();
            PutNewLine();
        }

        public string Prompt { get; set; } = ">>=: ";

        public IEnumerable<string> CommandList
        {
            get => (IEnumerable<string>) GetValue(CommandListProperty);
            set => SetValue(CommandListProperty, value);
        }

        public event EventHandler<StringEventArgs> ReadLine;

        public void PutNewLine(string text = "")
        {
            // Initialize Listeners
            currLine = new Line($"{Prompt} {text}");
            currLine.ReadLine += OnReadLine;
            currLine.UpPressed += OnUpPressed;
            currLine.DownPressed += OnDownPressed;
            currLine.TabPressed += OnTabPressed;
            StackPanel.Children.Add(currLine);

            historyIndex = history.Count;

            CleanPrevLine();
        }

        private void CleanPrevLine()
        {
            // Remove Listeners
            if (StackPanel.Children.Count <= 1) return;
            var prevLine = (Line) StackPanel.Children[StackPanel.Children.Count - 2];
            prevLine.ReadLine -= OnReadLine;
            prevLine.UpPressed -= OnUpPressed;
            prevLine.DownPressed -= OnDownPressed;
            prevLine.TabPressed -= OnTabPressed;
            prevLine.Input.IsReadOnly = true;
        }

        public void WriteLine(object? line)
        {
            StackPanel.Children.Add(new MessageLine(line?.ToString() ?? ""));
            CleanPrevLine();
        }

        public void WriteError(object error)
        {
            StackPanel.Children.Add(new ErrorLine(error.ToString()));
            CleanPrevLine();
        }

        private void OnTabPressed(object sender, EventArgs e)
        {
            var currText = currLine?.Text;
            var filteredCommands = CommandList.Where(x => x.StartsWith(currText)).ToList();
            if (filteredCommands.Count == 0) return;
            WriteLine(filteredCommands.Aggregate("", (acc, x) => $"{acc} {x}"));
            PutNewLine(currText ?? "");
        }

        private void OnDownPressed(object sender, EventArgs e)
        {
            if (historyIndex + 1 < history.Count)
                currLine.Text = history[++historyIndex];
        }

        private void OnUpPressed(object sender, EventArgs e)
        {
            if (historyIndex > 0)
                currLine.Text = history[--historyIndex];
        }

        protected virtual void OnReadLine(object sender, StringEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(e.Str))
                ReadLine?.Invoke(this, e);
            if (history.Count == 0 || e.Str != history.Last())
                history.Add(e.Str);
            PutNewLine();
        }


        private void StackPanel_OnMouseUp(object sender, MouseButtonEventArgs e)
        {
            currLine?.Input.Focus();
        }
    }
}