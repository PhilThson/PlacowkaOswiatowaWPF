using System;
using System.Reflection;

namespace PlacowkaOswiatowa.Domain.Commands
{
    public static class CommandUtils
    {
        internal static bool IsValidCommandParameter<T>(object o)
        {
            bool valid;
            if (o != null)
            {
                valid = o is T;

                if (!valid)
                    throw new ArgumentException("Brak zgodności obiektu i deklarowanego typu");

                return valid;
            }

            var t = typeof(T);

            //jeżeli parametr jest nullowalny
            if (Nullable.GetUnderlyingType(t) != null)
            {
                return true;
            }

            // czy jest to typ wartościowy
            valid = !t.GetTypeInfo().IsValueType;

            if (!valid)
                throw new ArgumentException("Typy wartościowe nie mogą być puste");

            return valid;
        }
    }
}
