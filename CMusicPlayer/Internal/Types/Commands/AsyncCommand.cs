using System;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CMusicPlayer.Internal.Types.Commands
{
    internal class AsyncCommand : ICommand
    {
        public Func<Task> ExecuteDelegate;

        public bool CanExecute(object parameter) => true;

        public AsyncCommand(Func<Task> executeDelegate)
        {
            ExecuteDelegate = executeDelegate;
        }

        public void Execute(object? parameter = null)
        {
            ExecuteDelegate?.Invoke();
        }

        #pragma warning disable 0067
        public event EventHandler CanExecuteChanged;
    }
}
