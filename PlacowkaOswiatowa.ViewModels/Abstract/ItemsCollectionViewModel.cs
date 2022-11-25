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
        //obiekt do operacji na bazie danych
        //protected readonly IPlacowkaRepository _repository;
        //komenda do załadowania encji
        //będzie podbięta pod przycisk 'Odśwież'
        private BaseCommand _LoadCommand;
        private BaseCommand _UpdateCommand;
        //kolekcja wszystkich towarów
        private ObservableCollection<T> _List;
        #endregion

        //public FakturyEntities FakturyEntites { get => fakturyEntities; }

        #region Konstruktor
        public ItemsCollectionViewModel(string displayName, IPlacowkaRepository repository)
            : base(repository)
        {
            base.DisplayName = displayName;
            //this.fakturyEntities = new FakturyEntities();
        }
        #endregion

        #region Właściwości i komendy
        public ICommand AddItemCommand => _AddItemCommand ??=
            new BaseCommand(() => OnRequestCreateView(ItemToCreateType));

        public ICommand LoadCommand => _LoadCommand ??= new BaseCommand(Load);

        public ObservableCollection<T> List
        {
            get => _List;
            //tutaj rezygnuje z automatycznego ładowanie, bo mam metody, które
            //asynchronicznie pobierają dane z bazy
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

        public ICommand UpdateCommand => _UpdateCommand ??= new BaseCommand(Update);
        #endregion

        #region Abstracts
        protected abstract Type ItemToCreateType { get; }
        public abstract void Load();
        public abstract void Update();
        #endregion
    }
}
