using System;

namespace CMusicPlayer.CLI.Commands
{
    internal class ArgumentError : Exception
    {
        public ArgumentError(string message) : base(message)
        {
        }
    }
}