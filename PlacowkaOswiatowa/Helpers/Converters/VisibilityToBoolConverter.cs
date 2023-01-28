using System;
using System.Globalization;
using System.Windows.Data;
using static PlacowkaOswiatowa.Domain.Helpers.CommonExtensions;

namespace PlacowkaOswiatowa.Helpers.Converters
{
    public class VisibilityToBoolConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var visibility = SafeToLower(value);
            return visibility == "visible" ? true : false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
