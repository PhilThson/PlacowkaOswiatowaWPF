using System;

namespace PlacowkaOswiatowa.Domain.Exceptions
{
    public class DataNotFoundException : Exception
    {
        public DataNotFoundException(string message) : base(message)
        { }
        public DataNotFoundException() : base("Nie znaleziono danych dla zadanych parametrów")
        { }
    }

    public class DataValidationException : Exception
    {
        public DataValidationException(string message) : base(message)
        {
        }
        public DataValidationException() : base("Wprowadzono niepoprawne dane")
        {
        }
    }
}
