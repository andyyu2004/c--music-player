using System.Windows;
using CMusicPlayer.UI.Utility;

namespace CMusicPlayer.UI.Properties
{
    /// <summary>
    /// Interaction logic for PreferencesPane.xaml
    /// </summary>
    internal partial class PreferencesPane : Window
    {
        public PreferencesPane()
        {
            InitializeComponent();
            new ApplicationBarEventHandler(this, WindowBar, Hide);
        }
    }
}
