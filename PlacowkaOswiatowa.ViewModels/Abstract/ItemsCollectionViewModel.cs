using AutoMapper;
using PlacowkaOswiatowa.Domain.Commands;
using PlacowkaOswiatowa.Domain.Helpers;
using PlacowkaOswiatowa.Domain.Interfaces.RepositoryInterfaces;
using PlacowkaOswiatowa.Domain.Resources;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace PlacowkaOswiatowa.ViewModels.Abstract
{
    public abstract class ItemsCollectionViewModel<T> : WorkspaceViewModel
        where T : IBaseEntity<object>
    {
        #region Pola prywatne i właściwości
        protected readonly IMapper _mapper;
        protected readonly ISignalHub<ViewHandler> _signalView;

        public string AddItemName { get; set; }

        private ICommand _AddItemCommand;
        private ICommand _LoadCommand;
        private ICommand _UpdateCommand;
        private ICommand _EditCommand;
        //private ICommand _OrderByCommand;
        //private ICommand _FilterCommand;
        //kolekcja wszystkich elementów
        private ObservableCollection<T> _List;
        protected IEnumerable<T> AllList { get; set; }
        public List<KeyValuePair<string, string>> ListOfItemsOrderBy { get; private set; }
        public List<KeyValuePair<string, string>> ListOfItemsFilter { get; private set; }
        #endregion

        #region Konstruktor
        public ItemsCollectionViewModel(IPlacowkaRepository repository, IMapper mapper,
            string displayName, string addItemName = null)
            : base(repository)
        {
            _mapper = mapper;
            _signalView = SignalHub<ViewHandler>.Instance;
            DisplayName = displayName;
            AddItemName = string.IsNullOrEmpty(addItemName) ?
                BaseResources.AddItem : addItemName;

            ListOfItemsOrderBy = SetListOfItemsOrderBy();
            ListOfItemsFilter = SetListOfItemsFilter();
        }
        #endregion

        #region Właściwości i komendy
        public ICommand AddItemCommand => _AddItemCommand ??=
            new BaseCommand(() => OnRequestCreateView(ItemToCreateType));
        public ICommand LoadCommand => _LoadCommand ??= new BaseCommand(Load);
        public ICommand UpdateCommand => _UpdateCommand ??= new BaseCommand(Update);
        public ICommand EditCommand => _EditCommand ??= new BaseCommand(Edit);

        //Zrezygnowałem z komend na rzecz wprowadzania filtrowania i sortowania
        //podczas PropertyChange wybranych ComboBoxów i TextBoxów
        //public ICommand OrderByCommand => _OrderByCommand ??= new BaseCommand(OrderBy);
        //public ICommand FilterCommand => _FilterCommand ??= new BaseCommand(Filter);

        public ObservableCollection<T> List
        {
            get => _List;
            set => SetProperty(ref _List, value);
        }

        private string _SelectedFilter;
        public string SelectedFilter
        {
            get => _SelectedFilter;
            set
            {
                if(value != _SelectedFilter)
                {
                    _SelectedFilter = value;
                    OnPropertyChanged();
                    Filter();
                }
            }
        }

        private string _SearchPhrase;
        public string SearchPhrase
        {
            get => _SearchPhrase;
            set
            {
                if(value != _SearchPhrase)
                {
                    _SearchPhrase = value;
                    OnPropertyChanged();
                    Filter();
                }
            }
        }

        private bool _OrderDescending;
        public bool OrderDescending
        {
            get => _OrderDescending;
            set
            {
                if (value != _OrderDescending)
                {
                    _OrderDescending = value;
                    OnPropertyChanged();
                    OrderBy();
                }
            }
        }

        private string _SelectedOrderBy;
        public string SelectedOrderBy
        {
            get => _SelectedOrderBy;
            set
            {
                if(value != _SelectedOrderBy)
                {
                    _SelectedOrderBy = value;
                    OnPropertyChanged();
                    OrderBy();
                }
            }
        }

        private T _SelectedItem;
        public T SelectedItem
        {
            get => _SelectedItem;
            set => SetProperty(ref _SelectedItem, value);
        }

        #endregion

        #region Metody
        private void OrderBy() 
        {
            if (string.IsNullOrEmpty(SelectedOrderBy))
                return;

            var selector = SetOrderBySelector();

            List = new ObservableCollection<T>(OrderDescending
                ? List.OrderByDescending(selector)
                : List.OrderBy(selector));
        }
        private void Filter() 
        {
            if (string.IsNullOrEmpty(SearchPhrase))
            {
                List = new ObservableCollection<T>(AllList);
                return;
            }

            if (string.IsNullOrEmpty(SelectedFilter))
                return;

            var predicate = SetFilterPredicate();

            List = new ObservableCollection<T>(AllList.Where(predicate));

            OrderBy();
        }
        private void Edit()
        {
            var viewHandler = new ViewHandler
            {
                Value = SelectedItem?.Id,
                ViewType = ItemToCreateType
            };
            _signalView.RaiseCreateView(this, viewHandler);
        }
        #endregion

        #region Abstracts
        //właściwość określająca jaki typ zakładki dany ViewModel może utworzyć
        protected abstract Type ItemToCreateType { get; }
        protected abstract void Load();
        protected abstract void Update();
        #endregion

        #region Virtuals
        protected virtual Func<T, string> SetOrderBySelector() =>
            item => string.Empty;
        protected virtual Func<T, bool> SetFilterPredicate() =>
            item => true;
        protected virtual List<KeyValuePair<string, string>> SetListOfItemsFilter() =>
            new List<KeyValuePair<string, string>>();
        protected virtual List<KeyValuePair<string, string>> SetListOfItemsOrderBy() =>
            new List<KeyValuePair<string, string>>();

        #endregion
    }
}
