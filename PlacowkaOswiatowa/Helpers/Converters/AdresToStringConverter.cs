using PlacowkaOswiatowa.Domain.Models;
using System;
using System.Globalization;
using System.Windows.Data;

namespace PlacowkaOswiatowa.Helpers.Converters
{
    [ValueConversion(typeof(Adres), typeof(string))]
    public class AdresToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var adres = value as Adres;

            return adres is not null ? $"{adres.Panstwo?.Nazwa}, " +
                $"ul. {adres.Ulica?.Nazwa} {adres.NumerDomu}" +
                $"{(string.IsNullOrEmpty(adres.NumerMieszkania) ? $"m. {adres.NumerMieszkania}" : "")}, " +
                $"{adres.KodPocztowy} {adres.Miejscowosc?.Nazwa}"
                : "Brak adresu do wyświetlenia";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
