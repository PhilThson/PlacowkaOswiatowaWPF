using PlacowkaOswiatowa.Domain.Commands;
using PlacowkaOswiatowa.Domain.Interfaces.RepositoryInterfaces;
using System;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PlacowkaOswiatowa.ViewModels.Abstract
{
    public abstract class SingleItemViewModel<T> : WorkspaceViewModel
    {
        #region Pola i właściwości
        public T Item { get; set; }
        #endregion

        #region Konstruktor
        public SingleItemViewModel(string displayName, IPlacowkaRepository repository)
            : base(repository)
        {
            base.DisplayName = displayName;
            _SaveAndCloseCommand = new BaseCommand(
                async () => await SaveAndClose(),
                SaveAndCloseCanExecute);
        }
        #endregion

        #region Komendy
        protected BaseCommand _SaveAndCloseCommand;
        public ICommand SaveAndCloseCommand { get => _SaveAndCloseCommand; }
        //{
        //    get
        //    {
        //        if (_SaveAndCloseCommand == null)
        //            _SaveAndCloseCommand = new BaseCommand(async () => await SaveAndClose());
        //        return _SaveAndCloseCommand;
        //    }
        //}
        #endregion

        #region Metody
        protected abstract Task SaveAsync();
        protected async Task SaveAndClose()
        {
            await SaveAsync();
            base.OnRequestClose();
        }
        protected virtual bool SaveAndCloseCanExecute() => true;
        #endregion
    }
}
