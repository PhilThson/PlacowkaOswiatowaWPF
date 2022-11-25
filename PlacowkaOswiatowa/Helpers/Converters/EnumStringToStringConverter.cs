using PlacowkaOswiatowa.Domain.Enums;
using PlacowkaOswiatowa.Domain.Helpers;
using System;
using System.Globalization;
using System.Windows.Data;

namespace PlacowkaOswiatowa.Helpers.Converters
{
    [ValueConversion(typeof(string), typeof(string))]
    public class EnumStringToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return 
                Enum.TryParse(value?.ToString(), out PrzedmiotEnum przedmiotEnum) ?
                przedmiotEnum.GetDescription() :
                Enum.TryParse(value?.ToString(), out EtatEnum etatEnum) ?
                etatEnum.GetDescription() :
                string.Empty;
            //if (Enum.TryParse(enumString, out PrzedmiotEnum przedmiotEnum))
            //    return przedmiotEnum.ToString();
            //if (Enum.TryParse(enumString, out EtatEnum etatEnum))
            //    return etatEnum.ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
