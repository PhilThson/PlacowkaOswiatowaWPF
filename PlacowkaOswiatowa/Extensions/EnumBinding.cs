using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;

namespace PlacowkaOswiatowa.Extensions
{
    public class EnumBinding : MarkupExtension
    {
        public Type EnumType { get; private set; }
        public EnumBinding(Type enumType)
        {
            if (enumType == null || !enumType.IsEnum)
                throw new Exception("Podany typ musi być Enum'em");

            EnumType = enumType;
        }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return Enum.GetValues(EnumType);
        }
    }
}
