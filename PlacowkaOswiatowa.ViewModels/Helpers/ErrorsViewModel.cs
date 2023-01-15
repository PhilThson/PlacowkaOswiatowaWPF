using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace PlacowkaOswiatowa.ViewModels.Helpers
{
    public class ErrorsViewModel : INotifyDataErrorInfo
    {
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
    }
}
