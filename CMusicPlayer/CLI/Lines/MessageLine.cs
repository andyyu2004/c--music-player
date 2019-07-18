using System.Windows.Media;

namespace CMusicPlayer.CLI.Lines
{
    public class MessageLine : Line
    {
        public MessageLine(string line)
        {
            Input.Text = line;
            Input.IsReadOnly = true;
            Input.Foreground = new SolidColorBrush(Color.FromRgb(255, 255, 255));
        }
    }
}