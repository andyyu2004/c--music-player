using CMusicPlayer.UI.Utility;

namespace CMusicPlayer.UI.Properties
{
    /// <summary>
    ///     Interaction logic for PreferencesPane.xaml
    /// </summary>
    internal partial class PreferencesPane
    {
        public PreferencesPane()
        {
            InitializeComponent();
            new ApplicationBarEventHandler(this, WindowBar, Close);
        }
    }
}