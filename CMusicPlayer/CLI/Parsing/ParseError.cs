using System;

namespace CMusicPlayer.CLI.Parsing
{
    internal class ParseError : Exception
    {
        public ParseError(string message) : base(message)
        {
        }
    }
}