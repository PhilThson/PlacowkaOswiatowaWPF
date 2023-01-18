using PlacowkaOswiatowa.Domain.Interfaces.CommonInterfaces;
using System;
using System.Threading.Tasks;
using System.Windows.Input;
using PlacowkaOswiatowa.Domain.Helpers;

namespace PlacowkaOswiatowa.ViewModels.Commands
{
    public class AsyncCommand : IAsyncCommand
    {
        readonly Func<Task> execute;
        readonly Func<bool>? canExecute;
        readonly Action<Exception>? onException;
        readonly bool continueOnCapturedContext = false;

        public AsyncCommand(
            Func<Task> execute,
            Func<bool>? canExecute = null,
            Action<Exception>? onException = null,
            bool continueOnCapturedContext = false)
        {
            this.execute = execute ?? throw new ArgumentNullException(nameof(execute));
            this.canExecute = canExecute;
            this.onException = onException;
            this.continueOnCapturedContext = continueOnCapturedContext;
        }

        public Task ExecuteAsync() => execute();
        public bool CanExecute(object? parameter = null) => canExecute?.Invoke() ?? true;

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }
        public void RaiseCanExecuteChanged()
        {
            CommandManager.InvalidateRequerySuggested();
        }
        void ICommand.Execute(object parameter) =>
            ExecuteAsync().SafeFireAndForget(onException, continueOnCapturedContext);
    }

    public class AsyncCommand<T> : IAsyncCommand<T>
    {
        readonly Func<T, Task> execute;
        readonly Func<object, bool>? canExecute;
        readonly Action<Exception>? onException;
        readonly bool continueOnCapturedContext;

        public AsyncCommand(Func<T, Task> execute,
                            Func<object, bool>? canExecute = null,
                            Action<Exception>? onException = null,
                            bool continueOnCapturedContext = false)
        {
            this.execute = execute ?? throw new ArgumentNullException(nameof(execute));
            this.canExecute = canExecute;
            this.onException = onException;
            this.continueOnCapturedContext = continueOnCapturedContext;
        }

        public Task ExecuteAsync(T parameter) => execute(parameter);
        public bool CanExecute(object parameter) => canExecute?.Invoke(parameter) ?? true;

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public void RaiseCanExecuteChanged()
        {
            CommandManager.InvalidateRequerySuggested();
        }

        void ICommand.Execute(object parameter)
        {
            ExecuteAsync((T)parameter).SafeFireAndForget(onException, continueOnCapturedContext);
        }
    }
}
