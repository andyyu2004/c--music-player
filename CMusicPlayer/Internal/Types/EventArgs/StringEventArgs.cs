namespace CMusicPlayer.Internal.Types.EventArgs
{
    public class StringEventArgs : System.EventArgs
    {
        public StringEventArgs(string str)
        {
            Str = str;
        }

        public string Str { get; }
    }
}