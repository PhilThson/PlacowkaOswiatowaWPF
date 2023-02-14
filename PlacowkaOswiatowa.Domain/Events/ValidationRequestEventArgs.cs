using System;

namespace PlacowkaOswiatowa.Domain.Models.Events
{
    public class ValidationRequestEventArgs : EventArgs
    {
        private readonly string? _propertyName;
        public ValidationRequestEventArgs(string? propertyName) 
        {
            _propertyName = propertyName;
        }

        public string? PropertyToValidate 
        { 
            get => _propertyName; 
        }
    }
}
