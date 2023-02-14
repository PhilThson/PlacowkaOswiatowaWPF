using PlacowkaOswiatowa.Domain.Models.Base;
using System;
using System.Globalization;
using System.Windows.Data;

namespace PlacowkaOswiatowa.Helpers.Converters
{
    [ValueConversion(typeof(BaseDictionaryEntity<byte>), typeof(string))]
    public class DictEntityToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var entity = value as BaseDictionaryEntity<byte>;
            return entity != null ? entity.Opis : "Brak";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
