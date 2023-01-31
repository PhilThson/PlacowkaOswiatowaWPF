using AutoMapper;
using PlacowkaOswiatowa.Domain.Commands;
using PlacowkaOswiatowa.Domain.Interfaces.CommonInterfaces;
using PlacowkaOswiatowa.Domain.Interfaces.RepositoryInterfaces;
using PlacowkaOswiatowa.Domain.Resources;
using PlacowkaOswiatowa.ViewModels.Commands;
using PlacowkaOswiatowa.ViewModels.Helpers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using static PlacowkaOswiatowa.Domain.Helpers.CommonExtensions;

namespace PlacowkaOswiatowa.ViewModels.Abstract
{
    public abstract class SingleItemViewModel<T> : WorkspaceViewModel, INotifyDataErrorInfo
        where T : new()
    {
        #region Pola i właściwości
        public T Item { get; set; }

        private string _AddItemName;
        public string AddItemName 
        {
            get => _AddItemName;
            set => SetProperty(ref _AddItemName, value);
        }

        protected readonly IMapper _mapper;
        #endregion

        #region Konstruktor
        public SingleItemViewModel(IServiceProvider serviceProvider, IMapper mapper,
            string displayName, string addItemName = null) 
            : base(serviceProvider)
        {
            _mapper = mapper;
            DisplayName = displayName;
            AddItemName = string.IsNullOrEmpty(addItemName) ?
               BaseResources.AddItem : addItemName;

            ErrorsChanged += OnErrorsChanged;
        }
        #endregion

        #region Komendy

        protected IAsyncCommand _SaveAndCloseCommand;
        public IAsyncCommand SaveAndCloseCommand => _SaveAndCloseCommand ??=
            new AsyncCommand(async () => await SaveAndClose(), SaveAndCloseCanExecute);

        protected ICommand _ClearFormCommand;
        public ICommand ClearFormCommand => _ClearFormCommand ??=
            new BaseCommand((o) => ClearForm(o));

        #endregion

        #region Metody

        protected abstract Task<bool> SaveAsync();
        protected async Task SaveAndClose()
        {
            CheckRequiredProperties();
            if (ValidationMessages.Count > 0)
                IsValidationVisible = true;
            if (HasErrors || IsValidationVisible)
                return;

            var isClosing = await SaveAsync();
            //w przypadku błędów formularza, zakładka ma pozostać otwarta
            if(isClosing)
                OnRequestClose();
        }

        //o aktywności przycisku do zapisu domyślnie decyduje posiadanie błędów
        protected virtual bool SaveAndCloseCanExecute() => !HasErrors;

        //domyślne zachowanie przycisku 'Wyczyść'
        //w parametrze komendy jest przesyłany bieżący ViewModel
        protected virtual void ClearForm(object obj) 
        {
            Item = new T();
            ClearAllErrors();
            ClearValidationMessages();
            if (obj == null) return;
            var viewModel = obj as WorkspaceViewModel;
            if (viewModel != null)
                foreach (var prop in viewModel.GetType().GetProperties())
                    OnPropertyChanged(prop.Name);
        }

        protected virtual void CheckRequiredProperties() { }

        //Metoda do walidacji wymaganych właściwości formularza
        protected void BaseCheckRequiredProperties(params string[] requiredProperties)
        {
            if (requiredProperties.Length < 1) return;
            foreach (var requiredProperty in requiredProperties)
            {
                if (string.IsNullOrEmpty(SafeToLower(GetPropertyValue(Item, requiredProperty))))
                {
                    var propertyName = requiredProperty.Split('.').Last();
                    ClearErrors(propertyName);
                    AddError(propertyName, $"Parametr {propertyName} jest wymagany");
                    OnPropertyChanged(propertyName);
                }
            }
        }

        private object GetPropertyValue(object src, string propName)
        {
            if (src == null) throw new ArgumentException("Brak obiektu do sprawdzenia", "src");
            if (propName == null) throw new ArgumentException("Brak właściwości do sprawdzenia", "propName");

            if (propName.Contains(".")) //dla właściwości zagnieżdżonych
            {
                var temp = propName.Split(new char[] { '.' }, 2);
                return GetPropertyValue(GetPropertyValue(src, temp[0]), temp[1]);
            }
            else
            {
                var prop = src.GetType().GetProperty(propName);
                return prop != null ? prop.GetValue(src, null) : null;
            }
        }

        //Domyślnie w klasie bazowej są obsługiwane zmiany w błędach poszczególnych właściwości
        //jeżeli jakaś klasa pochodna będzie chciała może nadpisać poniższą metodę
        //dzięki temu wystarczy wywołać ClearAllErrors i cały formularz zostanie pozbawiony błędów
        protected virtual void OnErrorsChanged(object sender, DataErrorsChangedEventArgs e)
        {
            _SaveAndCloseCommand.RaiseCanExecuteChanged();
            OnPropertyChanged(e.PropertyName);
        }

        #endregion

        //*******************************************************************************************

        #region Implementacja interfejsu INotifyDataErrorInfo

        #region Pola prywatne
        private readonly Dictionary<string, List<string>> _propertyErrors =
            new Dictionary<string, List<string>>();
        #endregion

        #region Właściwości
        public bool HasErrors => _propertyErrors.Any();
        #endregion

        #region Zdarzenia
        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;
        #endregion

        #region Metody

        public IEnumerable GetErrors(string propertyName) =>
            _propertyErrors.GetValueOrDefault(propertyName, null);


        public void AddErrorForPercentage(string propertyName) =>
            AddError(propertyName, $"Wartość musi być z zakresu od 0 do 100%");

        public void AddError(string propertyName, string errorMessage)
        {
            if (!_propertyErrors.ContainsKey(propertyName))
                _propertyErrors.Add(propertyName, new List<string>());

            _propertyErrors[propertyName].Add(errorMessage);
        }

        public void ClearErrors(string propertyName)
        {
            if (_propertyErrors.Remove(propertyName))
                RaiseErrorsChanged(propertyName);
        }

        public void ClearAllErrors()
        {
            foreach (var kvp in _propertyErrors)
            {
                _propertyErrors.Remove(kvp.Key);
                RaiseErrorsChanged(kvp.Key);
            }
        }

        public void RaiseErrorsChanged(string propertyName) =>
            ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));

        #endregion

        #endregion

        //*******************************************************************************************

        #region Obsługa ogólnych wiadomości walidacyjnych

        #region Prywatne pola
        private ObservableCollection<ValidationMessage> _ValidationMessages
            = new ObservableCollection<ValidationMessage>();

        private bool _IsValidationVisible = false;
        #endregion

        #region Właściwości
        public ObservableCollection<ValidationMessage> ValidationMessages
        {
            get => _ValidationMessages;
            set
            {
                _ValidationMessages = value;
                OnPropertyChanged(() => ValidationMessages);
            }
        }

        public bool IsValidationVisible
        {
            get => _IsValidationVisible;
            set
            {
                _IsValidationVisible = value;
                OnPropertyChanged(() => IsValidationVisible);
            }
        }
        #endregion

        #region Dodanie wiadomosci dot. poprawności formularza
        public virtual void AddValidationMessage(string propertyName, string msg)
        {
            _ValidationMessages.Add(new ValidationMessage { Message = msg, PropertyName = propertyName });
            IsValidationVisible = true;
        }
        #endregion

        #region Wyczyszczenie informacji dot. walidacji
        public virtual void ClearValidationMessages()
        {
            ValidationMessages.Clear();
            IsValidationVisible = false;
        }
        #endregion

        #endregion
    }
}
