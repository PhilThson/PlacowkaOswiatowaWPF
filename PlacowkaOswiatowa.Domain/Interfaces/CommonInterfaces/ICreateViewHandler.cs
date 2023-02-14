using System;

namespace PlacowkaOswiatowa.Domain.Interfaces.CommonInterfaces
{
    public interface ICreateViewHandler
    {
        public Type ViewType { get; set; }
        public object Value { get; set; }
    }
}
