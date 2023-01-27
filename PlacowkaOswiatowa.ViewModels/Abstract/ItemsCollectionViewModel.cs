using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using PlacowkaOswiatowa.Domain.Commands;
using PlacowkaOswiatowa.Domain.Exceptions;
using PlacowkaOswiatowa.Domain.Helpers;
using PlacowkaOswiatowa.Domain.Interfaces.RepositoryInterfaces;
using PlacowkaOswiatowa.Domain.Models.Base;
using PlacowkaOswiatowa.Domain.Resources;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Windows;
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
        private ICommand _DeleteCommand;
        //private ICommand _OrderByCommand;
        //private ICommand _FilterCommand;
        //kolekcja wszystkich elementów
        private ObservableCollection<T> _List;
        protected IEnumerable<T> AllList { get; set; }
        public List<KeyValuePair<string, string>> ListOfItemsOrderBy { get; private set; }
        public List<KeyValuePair<string, string>> ListOfItemsFilter { get; private set; }
        #endregion

        #region Konstruktor
        public ItemsCollectionViewModel(IServiceProvider serviceProvider, IMapper mapper,
            string displayName, string addItemName = null)
            : base(serviceProvider)
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

        #region Komendy
        public ICommand AddItemCommand => _AddItemCommand ??=
            new BaseCommand(() => OnRequestCreateView(ItemToCreateType));
        public ICommand LoadCommand => _LoadCommand ??= new BaseCommand(Load);
        public ICommand UpdateCommand => _UpdateCommand ??= new BaseCommand(Update);
        public ICommand EditCommand => _EditCommand ??= new BaseCommand(Edit);
        public ICommand DeleteCommand => _DeleteCommand ??= new BaseCommand(Delete);

        //Rezygnacja z komend na rzecz wprowadzania filtrowania i sortowania
        //podczas PropertyChange wybranych ComboBoxów i TextBoxów
        //public ICommand OrderByCommand => _OrderByCommand ??= new BaseCommand(OrderBy);
        //public ICommand FilterCommand => _FilterCommand ??= new BaseCommand(Filter);
        #endregion

        #region Właściwości

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
        private void Delete()
        {
            try
            {
                if (SelectedItem == null)
                    throw new DataValidationException("Nie wybrano rekordu do usunięcia");

                if (EntityType == null)
                    throw new DataValidationException("Nie można usunąć rekordu. Widok nie określa typu bazodanowego");

                if (MessageBox.Show("Czy na pewno chcesz usunąć wybrany rekord?", "Usuwanie",
                    MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.No)
                    return;

                using (var scope = _serviceProvider.CreateScope())
                {
                    var repository = scope.ServiceProvider.GetRequiredService<IPlacowkaRepository>();

                    var method = repository.GetType().GetMethod("GetById", BindingFlags.Public | BindingFlags.Instance);
                    var result = method.MakeGenericMethod(EntityType).Invoke(repository, new object[] { SelectedItem.Id });
                    var entity = result as BaseEntity<int>;
                    if (entity == null)
                        throw new DataNotFoundException($"Nie znaleziono rekordu o podanym identyfikatorze ({SelectedItem.Id})");

                    repository.Delete(entity);
                    repository.Save();
                }
                MessageBox.Show("Poprawnie usunięto rekord", "Sukces", MessageBoxButton.OK, MessageBoxImage.Information);
                Update();
            }
            catch(Exception e)
            {
                MessageBox.Show(e.Message, "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        #endregion

        #region Abstracts
        //właściwość określająca jaki typ zakładki dany ViewModel może utworzyć
        protected abstract void Load();
        protected abstract void Update();
        #endregion

        #region Virtuals
        protected virtual Type EntityType { get; }
        protected virtual Type ItemToCreateType { get; }
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
