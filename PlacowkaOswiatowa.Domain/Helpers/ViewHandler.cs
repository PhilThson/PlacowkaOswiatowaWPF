using System;

namespace PlacowkaOswiatowa.Domain.Helpers
{
    public class ViewHandler : EventArgs
    {
        public Type ViewType { get; set; }
        public object ItemId { get; set; }
        public bool IsSingleton { get; set; } = false;
    }
}
