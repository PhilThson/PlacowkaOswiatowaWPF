using System;
using System.Globalization;
using System.Windows.Data;

namespace PlacowkaOswiatowa.Helpers.Converters
{
    public class DecimalToPercentConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) return 0.0m;
            if (!decimal.TryParse(value.ToString(), out decimal val))
                return 0.0m;
            else
                return Math.Round(val * 100, 2);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) return 0.0m;
            if (!decimal.TryParse(value.ToString(), out decimal val))
                return 0.0m;
            else
                return Math.Round(val / 100, 2);
        }
    }
}
