using AutoMapper;
using PlacowkaOswiatowa.Domain.Commands;
using PlacowkaOswiatowa.Domain.Interfaces.CommonInterfaces;
using PlacowkaOswiatowa.Domain.Interfaces.RepositoryInterfaces;
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

namespace PlacowkaOswiatowa.ViewModels.Abstract
{
    public abstract class SingleItemViewModel<T> : WorkspaceViewModel, INotifyDataErrorInfo
    {
        #region Pola i właściwości
        public T Item { get; set; }
        protected readonly IMapper _mapper;
        #endregion

        #region Konstruktor
        public SingleItemViewModel(string displayName, 
            IPlacowkaRepository repository,
            IMapper mapper)
            : base(repository)
        {
            base.DisplayName = displayName;
            _mapper = mapper;
            _SaveAndCloseCommand = new AsyncCommand(
                async () => await SaveAndClose(),
                SaveAndCloseCanExecute);
        }
        #endregion

        #region Komendy

        protected IAsyncCommand _SaveAndCloseCommand;
        public IAsyncCommand SaveAndCloseCommand => _SaveAndCloseCommand;

        protected BaseCommand _ClearCommand;
        public ICommand ClearCommand => _ClearCommand;

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

        public void AddError(string propertyName, string errorMessage)
        {
            if (!_propertyErrors.ContainsKey(propertyName))
                _propertyErrors.Add(propertyName, new List<string>());

            _propertyErrors[propertyName].Add(errorMessage);
        }

        public void ClearErrors(string propertyName)
        {
            if (_propertyErrors.Remove(propertyName))
                OnErrorsChanged(propertyName);
        }

        public void ClearAllErrors()
        {
            foreach (var kvp in _propertyErrors)
            {
                _propertyErrors.Remove(kvp.Key);
                OnErrorsChanged(kvp.Key);
            }
        }

        public void OnErrorsChanged(string propertyName) =>
            ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));

        #endregion

        #endregion

        #region Obsługa wiadomości walidacyjnych

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

        #region AddBusinessRuleMessage Method
        //Dodanie wiadomosci dot. poprawności formularza
        public virtual void AddValidationMessage(string propertyName, string msg)
        {
            _ValidationMessages.Add(new ValidationMessage { Message = msg, PropertyName = propertyName });
            IsValidationVisible = true;
        }
        #endregion

        #region Clear Method
        //Wyczyszczenie informacji dot. walidacji
        public virtual void Clear()
        {
            ValidationMessages.Clear();
            IsValidationVisible = false;
        }
        #endregion

        #endregion
    }
}
