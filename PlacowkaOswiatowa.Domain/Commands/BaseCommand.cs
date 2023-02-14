using System;
using System.CodeDom.Compiler;
using System.Windows.Input;

namespace PlacowkaOswiatowa.Domain.Commands
{
    public class BaseCommand : ICommand
    {
        private readonly Action<object> _command;
        private readonly Func<bool> _canExecute;
        public event EventHandler CanExecuteChanged;
        //{
        //    add { CommandManager.RequerySuggested += value; }
        //    remove { CommandManager.RequerySuggested -= value; }
        //}
        public BaseCommand(Action command, Func<bool> canExecute = null)
            : this(o => command())
        {
            if (command == null)
                throw new ArgumentNullException("command");
            _canExecute = canExecute ?? (() => true);
            //_command = command;
        }

        public BaseCommand(Action<object> command)
        {
            _command = command ?? throw new ArgumentNullException(nameof(command));
        }

        public BaseCommand(Action<object> command, Func<bool> canExecute = null) 
            : this(command)
        {
            _canExecute = canExecute ?? (() => true);
        }

        public void Execute(object parameter) => _command(parameter);

        public bool CanExecute(object parameter) //=> _canExecute();
        {
            if (_canExecute == null)
                return true;
            return _canExecute();
        }
        //rozgłoszenie eventu CanExecuteChanged:
        // wywołuje metodę CanExecute - sprawdza warunek czy komenda może być wykonana
        // warunek sprawdzający został podany przy tworzeniu komendy
        public void OnCanExecuteChanged() =>
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
    }

    //public class BaseCommand<T> : BaseCommand
    //{
    //    public BaseCommand(Action<T> command)
    //        : base(o =>
    //        {
    //            if (CommandUtils.IsValidCommandParameter<T>(o))
    //                command((T)o);
    //        })
    //    { 
    //        if (command == null)
    //            throw new ArgumentNullException(nameof(command));
    //    }

    //    public BaseCommand(Action command, Func<bool> canExecute = null) 
    //        : base(command, canExecute)
    //    {
    //        if (command == null)
    //            throw new ArgumentNullException(nameof(command));
    //        if (canExecute == null)
    //            throw new ArgumentNullException(nameof(canExecute));
    //    }
    //}
}
