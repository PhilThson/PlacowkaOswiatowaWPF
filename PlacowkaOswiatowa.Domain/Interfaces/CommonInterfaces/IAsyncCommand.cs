using System.Threading.Tasks;
using System.Windows.Input;

namespace PlacowkaOswiatowa.Domain.Interfaces.CommonInterfaces
{
    public interface IAsyncCommand : ICommand
    {
        Task ExecuteAsync();
        void RaiseCanExecuteChanged();
    }

    public interface IAsyncCommand<T> : ICommand
    {
        Task ExecuteAsync(T parameter);
        void RaiseCanExecuteChanged();
    }
}
