using System;
using System.Windows.Input;

namespace PlacowkaOswiatowa.Domain.Commands
{
    public class BaseCommand : ICommand
    {
        private readonly Action _command;
        private readonly Func<bool> _canExecute;
        public event EventHandler CanExecuteChanged;
        //{
        //    add { CommandManager.RequerySuggested += value; }
        //    remove { CommandManager.RequerySuggested -= value; }
        //}
        public BaseCommand(Action command, Func<bool> canExecute = null)
        {
            if (command == null)
                throw new ArgumentNullException("command");
            _canExecute = canExecute ?? (() => true);
            _command = command;
        }
        public void Execute(object parameter) =>
            _command();

        public bool CanExecute(object parameter) =>
            _canExecute();
        //{
        //    if (_canExecute == null)
        //        return true;
        //    return _canExecute();
        //}
        //rozgłoszenie eventu CanExecuteChanged:
        // wywołuje metodę CanExecute - sprawdza warunek czy komenda może być wykonana
        // warunek sprawdzający został podany przy tworzeniu komendy
        public void OnCanExecuteChanged() =>
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
    }
}
