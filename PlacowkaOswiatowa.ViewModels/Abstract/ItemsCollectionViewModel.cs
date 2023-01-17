﻿using AutoMapper;
using PlacowkaOswiatowa.Domain.Commands;
using PlacowkaOswiatowa.Domain.Interfaces.RepositoryInterfaces;
using PlacowkaOswiatowa.Domain.Resources;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace PlacowkaOswiatowa.ViewModels.Abstract
{
    public abstract class ItemsCollectionViewModel<T> : WorkspaceViewModel
    {
        #region Pola prywatne i właściwości
        protected readonly IMapper _mapper;

        public string AddItemName { get; set; }

        private ICommand _AddItemCommand;
        private ICommand _LoadCommand;
        private ICommand _UpdateCommand;
        private ICommand _OrderByCommand;
        private ICommand _FilterCommand;
        //kolekcja wszystkich elementów
        private ObservableCollection<T> _List;
        protected List<T> AllList { get; set; }
        public List<KeyValuePair<string, string>> ListOfItemsOrderBy { get; private set; }
        public List<KeyValuePair<string, string>> ListOfItemsFilter { get; private set; }
        #endregion

        #region Konstruktor
        public ItemsCollectionViewModel(IPlacowkaRepository repository, IMapper mapper,
            string displayName, string addItemName = null)
            : base(repository)
        {
            _mapper = mapper;
            base.DisplayName = displayName;
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
        public ICommand OrderByCommand => _OrderByCommand ??= new BaseCommand(OrderBy);
        public ICommand FilterCommand => _FilterCommand ??= new BaseCommand(Filter);

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

        private string _SelectedOrderBy;
        public string SelectedOrderBy
        {
            get => _SelectedOrderBy;
            set => SetProperty(ref _SelectedOrderBy, value);
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
            set => SetProperty(ref _OrderDescending, value);
        }

        private T _SelectedItem;
        public T SelectedItem
        {
            get => _SelectedItem;
            set => SetProperty(ref _SelectedItem, value);
        }

        #endregion

        #region Abstracts
        //właściwość
        protected abstract Type ItemToCreateType { get; }
        protected abstract void Load();
        protected abstract void Update();
        protected virtual void OrderBy() { return; }
        protected virtual void Filter() { return; }
        protected virtual List<KeyValuePair<string, string>> SetListOfItemsFilter() =>
            new List<KeyValuePair<string, string>>();
        protected virtual List<KeyValuePair<string, string>> SetListOfItemsOrderBy() =>
            new List<KeyValuePair<string, string>>();
        #endregion
    }
}
