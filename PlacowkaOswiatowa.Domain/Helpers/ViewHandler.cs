using System;

namespace PlacowkaOswiatowa.Domain.Helpers
{
    public class ViewHandler : EventArgs
    {
        public Type ViewType { get; private set; }
        public object ItemId { get; private set; }
        public bool IsModal { get; private set; }
        public bool IsSingleton { get; private set; }

        //w celu zablokowania możliwości edycji parametrów handlera
        //podczas jego przesyłania
        public ViewHandler(Type viewType, object itemId = null,
            bool isModal = false, bool isSingleton = false)
        {
            ViewType = viewType;
            ItemId = itemId;
            IsModal = isModal;
            IsSingleton = isSingleton;
        }
    }
}
