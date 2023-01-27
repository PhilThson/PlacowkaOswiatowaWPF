using PlacowkaOswiatowa.Domain.Commands;
using PlacowkaOswiatowa.Domain.Interfaces.RepositoryInterfaces;
using System;
using System.Windows.Input;

namespace PlacowkaOswiatowa.ViewModels.Abstract
{
    //Klasa zawierająca dane wspólne dla wszystkich zakładek
    public abstract class WorkspaceViewModel : BaseViewModel
    {
        #region Pola i właściwości
        protected readonly IServiceProvider _serviceProvider;

        //ze względu na asynchroniczne tworzenie zakładek do edycji, 
        //wyświetlana nazwa zakładki może się zmienić już po jej utworzeniu
        private string _displayName;
        public string DisplayName 
        { 
            get => _displayName; 
            set => SetProperty(ref _displayName, value); 
        }
        #endregion

        #region Konstruktor
        public WorkspaceViewModel(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }
        #endregion

        #region Komendy
        private BaseCommand _CloseCommand;

        public ICommand CloseCommand
        {
            get
            {
                if (_CloseCommand == null)
                    _CloseCommand = new BaseCommand(() => OnRequestClose());
                return _CloseCommand;
            }
        }
        #endregion

        #region Eventy

        //Event do zamknięcia zakładki
        public event EventHandler RequestClose;
        protected void OnRequestClose()
        {
            EventHandler handler = RequestClose;
            if (handler != null)
                handler(this, EventArgs.Empty);
        }

        //Event do wywołania nowej zakładki z już otwartej zakładki
        //można by było też dać Action<WorkspaceViewModel> i bezpośrednio przesyłać utworzony obiekt
        //konkretnego viewmodelu, a nie przez pomocniczą klasę
        //Za to w klasie można jeszcze coś dodać...
        //albo (jak tutaj) odrazu przesłać typ potrzebnej zakładki
        public event EventHandler<Type> RequestCreateView;
        protected void OnRequestCreateView(Type type) =>
            RequestCreateView?.Invoke(this, type);

        #endregion
    }
}