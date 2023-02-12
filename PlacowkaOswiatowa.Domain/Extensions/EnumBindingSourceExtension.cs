using System;
using System.Data;
using System.Linq;
using System.Windows.Markup;

namespace PlacowkaOswiatowa.Domain.Extensions
{
    public class EnumBindingSourceExtension : MarkupExtension
    {
        public Type EnumType { get; set; }

        public EnumBindingSourceExtension(Type enumType)
        {
            if (enumType is null || !enumType.IsEnum)
                throw new Exception("Należy podać prawidłowy typ Enum");
            EnumType = enumType;
        }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return Enum.GetValues(EnumType).Cast<Enum>().Select(e => e.GetDescription()).ToList();
        }
    }
}
