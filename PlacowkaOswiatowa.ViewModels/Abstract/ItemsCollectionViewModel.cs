using PlacowkaOswiatowa.Domain.Commands;
using PlacowkaOswiatowa.Domain.Interfaces.RepositoryInterfaces;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace PlacowkaOswiatowa.ViewModels.Abstract
{
    public abstract class ItemsCollectionViewModel<T> : WorkspaceViewModel
    {
        #region Pola prywatne
        private BaseCommand _AddItemCommand;
        private BaseCommand _LoadCommand;
        private BaseCommand _UpdateCommand;
        //kolekcja wszystkich elementów
        private ObservableCollection<T> _List;
        #endregion

        #region Konstruktor
        public ItemsCollectionViewModel(string displayName, IPlacowkaRepository repository)
            : base(repository)
        {
            base.DisplayName = displayName;
        }
        #endregion

        #region Właściwości i komendy
        public ICommand AddItemCommand => _AddItemCommand ??=
            new BaseCommand(() => OnRequestCreateView(ItemToCreateType));
        public ICommand LoadCommand => _LoadCommand ??= new BaseCommand(Load);
        public ICommand UpdateCommand => _UpdateCommand ??= new BaseCommand(Update);

        public ObservableCollection<T> List
        {
            get => _List;
            //tutaj rezygnuje z automatycznego ładowania, bo jest metoda, która
            //asynchronicznie pobiera dane z bazy
            //{
            //    if (_List == null)
            //    {
            //        Load();
            //    }
            //    return _List;
            //}
            set
            {
                _List = value;
                OnPropertyChanged(() => List);
            }
        }

        #endregion

        #region Abstracts
        //właściwość
        protected abstract Type ItemToCreateType { get; }
        public abstract void Load();
        public abstract void Update();
        #endregion
    }
}
