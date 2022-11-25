using System;
using System.ComponentModel;
using System.Linq.Expressions;

namespace PlacowkaOswiatowa.ViewModels.Abstract
{
    public abstract class BaseViewModel : INotifyPropertyChanged
    {

        #region Propertychanged
        protected void OnPropertyChanged<T>(Expression<Func<T>> action)
        {
            var propertyName = GetPropertyName(action);
            OnPropertyChanged(propertyName);
        }
        private static string GetPropertyName<T>(Expression<Func<T>> action)
        {
            var expression = (MemberExpression)action.Body;
            var propertyName = expression.Member.Name;
            return propertyName;
        }
        private void OnPropertyChanged(string propertyName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        public virtual void Dispose()
        { }
    }
}
