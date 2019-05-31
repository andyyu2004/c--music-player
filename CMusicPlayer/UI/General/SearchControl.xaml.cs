using System;
using System.Windows;
using System.Windows.Controls;

namespace CMusicPlayer.UI.General
{
    /// <summary>
    /// Interaction logic for SearchControl.xaml
    /// </summary>
    internal partial class SearchControl
    {
        public event EventHandler<TextChangedEventArgs> TextChanged;

        public SearchControl() => InitializeComponent();

        public bool FocusInput() => SearchBox.Focus();

        private void OnTextChanged(object sender, TextChangedEventArgs e)
            => TextChanged?.Invoke(sender, e);

        private void ClearText(object sender, RoutedEventArgs e) => SearchBox.Text = string.Empty;
    }
}
