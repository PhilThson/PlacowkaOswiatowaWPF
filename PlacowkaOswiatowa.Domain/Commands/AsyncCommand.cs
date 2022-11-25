using PlacowkaOswiatowa.Domain.Commands;
using PlacowkaOswiatowa.Domain.Interfaces.CommonInterfaces;
using System;
using System.Threading.Tasks;

namespace PlacowkaOswiatowa.ViewModels.Commands
{
    public class AsyncCommand : BaseAsyncCommand, IAsyncCommand
    {
        private readonly Func<Task> _command;
        public AsyncCommand(Func<Task> command)
        {
            _command = command;
        }
        public override bool CanExecute(object parameter)
        {
            return true;
        }
        public override Task ExecuteAsync(object parameter)
        {
            return _command();
        }
    }

}
