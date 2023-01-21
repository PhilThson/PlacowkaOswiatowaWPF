using System;
using System.Globalization;
using System.Windows.Data;

namespace PlacowkaOswiatowa.Helpers.Converters
{
    [ValueConversion(typeof(object), typeof(string))]
    public class ObjectToEnableConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value != null ? "True" : "False";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
