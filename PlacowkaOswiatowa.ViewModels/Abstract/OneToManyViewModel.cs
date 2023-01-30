using AutoMapper;
using PlacowkaOswiatowa.Domain.Commands;
using PlacowkaOswiatowa.Domain.Resources;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace PlacowkaOswiatowa.ViewModels.Abstract
{
    public abstract class OneToManyViewModel<T, TMany> : SingleItemViewModel<T>
        where T : class, new()
    {
        #region Pola i właściwości

        private ObservableCollection<TMany> _AllList;
        public ObservableCollection<TMany> AllList
        {
            get => _AllList;
            set => SetProperty(ref _AllList, value);
        }

        private TMany _SelectedItem;
        public TMany SelectedItem
        {
            get => _SelectedItem;
            set => SetProperty(ref _SelectedItem, value);
        }

        public string AllDisplayName { get; set; }
        public string AddItemButtonContent { get; set; }
        public string EditItemButtonContent { get; set; }

        #endregion

        #region Komendy
        private ICommand _AddItemCommand;
        public ICommand AddItemCommand => _AddItemCommand ??=
            new BaseCommand(ShowAddView);

        private ICommand _EditItemCommand;
        public ICommand EditItemCommand => _EditItemCommand ??=
            new BaseCommand((item) => ShowEditView(item));
        #endregion

        #region Konstruktor
        public OneToManyViewModel(IServiceProvider serviceProvider, IMapper mapper,
            string displayName, string addItemName = null, 
            string addItemButtonContent = null, string editItemButtonContent = null,
            string allName = null)
            : base(serviceProvider, mapper, displayName, addItemName)
        {
            AllDisplayName = allName ?? BaseResources.AllItems;
            AddItemButtonContent = addItemButtonContent ?? BaseResources.AddItem;
            EditItemButtonContent = editItemButtonContent ?? BaseResources.EditItem;
        }
        #endregion


        #region Metody
        protected abstract void ShowAddView();
        protected abstract void ShowEditView(object item);

        #endregion
    }
}
