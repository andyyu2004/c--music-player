using System;

namespace CMusicPlayer.CLI.Interpreting
{
    internal class InterpretationError : Exception
    {
        public InterpretationError(string message) : base(message)
        {
            
        }
    }
}