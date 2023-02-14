using System;
using System.Windows.Input;

namespace PlacowkaOswiatowa.Domain.Commands
{
    public class Command : ICommand
    {
        readonly Func<object, bool>? canExecute;
        readonly Action<object> execute;

        public Command(Action<object> execute)
        {
            this.execute = execute ?? throw new ArgumentNullException(nameof(execute));
        }

        public Command(Action execute) : this(o => execute())
        {
            if (execute == null)
                throw new ArgumentNullException(nameof(execute));
        }

        public Command(Action<object> execute, Func<object, bool>? canExecute) : this(execute)
        {
            this.canExecute = canExecute;
        }

        public Command(Action execute, Func<bool>? canExecute) : this(o => execute())
        {
            if (execute == null)
                throw new ArgumentNullException(nameof(execute));

            if (canExecute != null)
                this.canExecute = o => canExecute();
        }

        public bool CanExecute(object parameter) => canExecute?.Invoke(parameter) ?? true;


        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public void Execute(object parameter) => execute(parameter);

        public void RaiseCanExecuteChanged() =>
            CommandManager.InvalidateRequerySuggested();
    }
}
