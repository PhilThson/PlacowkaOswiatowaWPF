using PlacowkaOswiatowa.ViewModels.Abstract;

namespace PlacowkaOswiatowa.ViewModels.Helpers
{
    public class ValidationMessage : BaseViewModel
    {
        #region Właściwości
        private string _propertyName;
        
        public string PropertyName
        {
            get { return _propertyName; }
            set
            {
                _propertyName = value;
                OnPropertyChanged(() => PropertyName);
            }
        }
        private string _message;
        public string Message
        {
            get { return _message; }
            set
            {
                _message = value;
                OnPropertyChanged(() => Message);
            }
        }
        #endregion
    }
}
