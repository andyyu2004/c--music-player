using System;
using System.Windows.Input;

namespace CMusicPlayer.Internal.Types.Commands
{
    internal class Command : ICommand
    {
        private readonly Action action;

        public Command(Action action) => this.action = action;

        public bool CanExecute(object parameter) => true;

        public void Execute(object parameter) => action();

        #pragma warning disable 0067
        public event EventHandler CanExecuteChanged;
    }
}
