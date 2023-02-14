using System;
using System.Globalization;
using System.Windows.Data;

namespace PlacowkaOswiatowa.Helpers.Converters
{
    [ValueConversion(typeof(DateTime?), typeof(string))]
    public class DateToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var dateTime = value as DateTime?;
            return dateTime.HasValue ? dateTime.Value.ToString("dd.MM.yyyy") : string.Empty;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var strValue = value.ToString();
            DateTime resultDateTime;
            return DateTime.TryParseExact(strValue, "dd.MM.yyyy", new CultureInfo("pl-PL"), DateTimeStyles.None, out resultDateTime) ? 
                resultDateTime : value;
        }
    }
}
