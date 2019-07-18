using System;

namespace CMusicPlayer.CLI.Lexing
{
    internal class LexError : Exception
    {
        public LexError(string message) : base(message)
        {
        }
    }
}