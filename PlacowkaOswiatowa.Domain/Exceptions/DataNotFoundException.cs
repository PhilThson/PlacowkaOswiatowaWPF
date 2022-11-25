using System;

namespace PlacowkaOswiatowa.Domain.Exceptions
{
    public class DataNotFoundException : Exception
    {
        public DataNotFoundException(string wiadomosc) : base(wiadomosc)
        { }
        public DataNotFoundException() : base("Nie znaleziono danych dla zadanych parametrów")
        { }
    }
}
