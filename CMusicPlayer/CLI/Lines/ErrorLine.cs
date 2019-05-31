using System.Windows.Media;

namespace CMusicPlayer.CLI.Lines
{
    public class ErrorLine : Line
    {
        public ErrorLine(string error)
        {
            Input.Text = error;
            Input.IsReadOnly = true;
            Input.Foreground = new SolidColorBrush(Color.FromRgb(255, 0, 0));
        }
    }
}
