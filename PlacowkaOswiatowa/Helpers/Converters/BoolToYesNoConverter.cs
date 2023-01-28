using System;
using System.Globalization;
using System.Windows.Data;
using static PlacowkaOswiatowa.Domain.Helpers.CommonExtensions;

namespace PlacowkaOswiatowa.Helpers.Converters
{
    public class BoolToYesNoConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) return "brak danych";
            if (!bool.TryParse(SafeToLower(value), out bool yesNo))
                return "brak danych";
            return yesNo == true ? "Tak" : "Nie";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
