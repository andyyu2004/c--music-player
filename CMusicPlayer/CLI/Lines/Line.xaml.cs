using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using CMusicPlayer.Internal.Types.EventArgs;

namespace CMusicPlayer.CLI.Lines
{
    /// <summary>
    ///     Interaction logic for Line.xaml
    /// </summary>
    public partial class Line // : UserControl
    {
        private readonly Dictionary<Key, Action> actionMap;
        private readonly string prompt = string.Empty;

        public Line()
        {
            InitializeComponent();
            Input.CaretIndex = prompt.Length;
            //            FocusManager.SetFocusedElement(this, Input);
            actionMap = GenerateActionMap();
        }

        public Line(string prompt) : this()
        {
            this.prompt = prompt;
            Input.AppendText(prompt);
        }

        public string Text
        {
            get => Input.Text.Substring(prompt.Length);
            set
            {
                Input.Text = $"{prompt}{value}";
                Input.CaretIndex = Input.Text.Length;
            }
        }

        public event EventHandler<StringEventArgs> ReadLine;
        public event EventHandler UpPressed;
        public event EventHandler DownPressed;
        public event EventHandler TabPressed;

        //        public void SetText(string text)
        //        {
        //            Input.Text = $"{_pretext}{text}";
        //            Input.CaretIndex = Input.Text.Length;
        //        }

        private void OnKeyDown(object sender, KeyEventArgs e)
        {
            // Doesn't allow modification to prompt
            if (Input.CaretIndex <= prompt.Length && (e.Key == Key.Back || e.Key == Key.Left)) e.Handled = true;

            // Ignores other actions if contained in ActionMap
            if (!actionMap.ContainsKey(e.Key)) return;
            e.Handled = true;
            actionMap[e.Key]();
        }

        private Dictionary<Key, Action> GenerateActionMap()
        {
            return new Dictionary<Key, Action>
            {
                {
                    Key.Return,
                    () => ReadLine?.Invoke(this, new StringEventArgs(Text))
                },
                {
                    Key.Up,
                    () => UpPressed?.Invoke(this, EventArgs.Empty)
                },
                {
                    Key.Down,
                    () => DownPressed?.Invoke(this, EventArgs.Empty)
                },
                {
                    Key.Tab,
                    () => TabPressed?.Invoke(this, EventArgs.Empty)
                }
            };
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            Input.Focus();
        }

        private void OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            // Doesn't let mouse move cursor like most terminals
            e.Handled = true;
        }
    }
}