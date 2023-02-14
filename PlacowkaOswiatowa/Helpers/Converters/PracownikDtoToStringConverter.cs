using PlacowkaOswiatowa.Domain.DTOs;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace PlacowkaOswiatowa.Helpers.Converters
{
    public class PracownikDtoToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var pracownik = value as PracownikDto;
            if (pracownik == null) return "Brak danych do wyświetlenia";
            return $"{pracownik.Imie} {pracownik.Nazwisko}, {pracownik.Pesel}";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            //throw new NotImplementedException();
            return null;
        }
    }
}
