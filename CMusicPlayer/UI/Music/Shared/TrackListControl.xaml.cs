using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CMusicPlayer.UI.Music.Shared
{
    /// <summary>
    /// Interaction logic for TrackListControl.xaml
    /// </summary>
    public partial class TrackListControl : UserControl
    {
        public TrackListControl()
        {
            InitializeComponent();
        }

        private void OnTrackDoubleClicked(object sender, MouseButtonEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
