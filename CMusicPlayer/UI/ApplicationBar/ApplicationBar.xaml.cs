using System;
using System.Windows;
using System.Windows.Input;

namespace CMusicPlayer.UI.ApplicationBar
{
    /// <summary>
    ///     Interaction logic for ApplicationBar.xaml
    /// </summary>
    public partial class ApplicationBar : IApplicationBar
    {
        public static readonly DependencyProperty BarTitleProperty = DependencyProperty.Register(
            nameof(BarTitle), typeof(string), typeof(ApplicationBar), new PropertyMetadata(""));

        public ApplicationBar()
        {
            InitializeComponent();
            DataContext = this;
        }

        public string BarTitle
        {
            get => (string) GetValue(BarTitleProperty);
            set => SetValue(BarTitleProperty, value);
        }

        public event EventHandler BarMouseDown;
        public event EventHandler MinimizeClicked;
        public event EventHandler MaximizeClicked;
        public event EventHandler CloseClicked;

        private void OnBarMouseDown(object sender, MouseButtonEventArgs e)
        {
            BarMouseDown?.Invoke(this, EventArgs.Empty);
        }

        private void OnBarDoubleClick(object sender, MouseEventArgs e)
        {
            MaximizeClicked?.Invoke(this, EventArgs.Empty);
        }

        private void OnMinimizeClicked(object sender, EventArgs e)
        {
            MinimizeClicked?.Invoke(this, EventArgs.Empty);
        }

        private void OnMaximizeClicked(object sender, EventArgs e)
        {
            MaximizeClicked?.Invoke(this, EventArgs.Empty);
        }

        private void OnCloseClicked(object sender, EventArgs e)
        {
            CloseClicked?.Invoke(this, EventArgs.Empty);
        }
    }
}