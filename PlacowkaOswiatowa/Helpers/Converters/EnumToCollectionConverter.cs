using PlacowkaOswiatowa.Domain.Helpers;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows.Data;
using System.Windows.Markup;

namespace PlacowkaOswiatowa.Helpers.Converters
{
    [ValueConversion(typeof(Enum), typeof(IEnumerable<string>))]
    public class EnumToCollectionConverter : MarkupExtension, IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Type objType = value.GetType();
            return Enum.GetValues(objType).Cast<Enum>().Select(e => e.GetDescription()).ToList();
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value.ToString();
        }
        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return this;
        }
    }
}
