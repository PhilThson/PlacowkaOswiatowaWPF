using PlacowkaOswiatowa.Domain.DTOs;
using System;
using System.Globalization;
using System.Windows.Data;

namespace PlacowkaOswiatowa.Helpers.Converters
{
    [ValueConversion(typeof(AdresDto), typeof(string))]
    public class AdresDtoToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var adres = value as AdresDto;
            
            return adres != null ? adres.ToString()
                : "Brak adresu do wyświetlenia";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
