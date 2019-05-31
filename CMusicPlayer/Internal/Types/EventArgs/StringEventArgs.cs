namespace CMusicPlayer.Internal.Types.EventArgs
{
    public class StringEventArgs : System.EventArgs
    {
        public string Str { get; }

        public StringEventArgs(string str)
        {
            Str = str;
        }
    }
}
