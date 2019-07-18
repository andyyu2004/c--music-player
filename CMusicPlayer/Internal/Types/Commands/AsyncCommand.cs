using System;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CMusicPlayer.Internal.Types.Commands
{
    internal class AsyncCommand : ICommand
    {
        public Func<Task> ExecuteDelegate;

        public AsyncCommand(Func<Task> executeDelegate)
        {
            ExecuteDelegate = executeDelegate;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object? parameter = null)
        {
            ExecuteDelegate?.Invoke();
        }
#pragma warning disable 0067
        public event EventHandler CanExecuteChanged;
    }
}