using PlacowkaOswiatowa.Domain.MessageBroker;
using PlacowkaOswiatowa.ViewModels.Helpers;
using System;
using System.Collections.ObjectModel;

namespace PlacowkaOswiatowa.ViewModels.Abstract
{
    //Model pośredni do obsługi wiadomości walidacyjnych
    //TODO: Przenieść to do SingleItemViewModel
    public abstract class MediateViewModel : BaseViewModel
    {
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

        #region DisplayStatusMessage Method
        public virtual void DisplayStatusMessage(string msg = "")
        {
            MessageBroker.Instance.SendMessage(MessageBrokerMessages.DISPLAY_STATUS_MESSAGE, msg);
        }
        #endregion

        #region PublishException Method
        // Dodanie informacji diagnostycznych
        public void PublishException(Exception ex)
        {
            ExceptionManager.Instance.Publish(ex);
        }
        #endregion

        #region Close Method
        public virtual void Close(bool wasCancelled = true)
        {
            MessageBroker.Instance.SendMessage(MessageBrokerMessages.CLOSE_USER_CONTROL, wasCancelled);
        }
        #endregion

        #region Dispose Method
        public virtual void Dispose()
        {
        }
        #endregion
    }
}
