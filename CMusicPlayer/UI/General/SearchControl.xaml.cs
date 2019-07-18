using System;
using System.Windows;
using System.Windows.Controls;

namespace CMusicPlayer.UI.General
{
    /// <summary>
    ///     Interaction logic for SearchControl.xaml
    /// </summary>
    internal partial class SearchControl
    {
        public SearchControl()
        {
            InitializeComponent();
        }

        public event EventHandler<TextChangedEventArgs> TextChanged;

        public bool FocusInput()
        {
            return SearchBox.Focus();
        }

        private void OnTextChanged(object sender, TextChangedEventArgs e)
        {
            TextChanged?.Invoke(sender, e);
        }

        private void ClearText(object sender, RoutedEventArgs e)
        {
            SearchBox.Text = string.Empty;
        }
    }
}