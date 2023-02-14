using PlacowkaOswiatowa.Domain.Interfaces.RepositoryInterfaces;
using System;
using System.Globalization;
using System.Windows.Data;

namespace PlacowkaOswiatowa.Helpers.Converters
{
    //Konwerter do zablokowania możliwości zmiany pracownika lub pracodawcy
    //podczas edycji umowy
    //ew. do wyłączenia przycisku dodawania adresu ucznia, po dodaniu adresu
    public class BaseEntityToIsEnabledConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var baseEntity = value as IBaseEntity<object>;
            if (baseEntity == null) return true;
            var id = System.Convert.ToInt32(baseEntity.Id);
            return id == default ? true : false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
