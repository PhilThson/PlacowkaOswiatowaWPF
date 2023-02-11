using PlacowkaOswiatowa.Domain.DTOs;
using PlacowkaOswiatowa.Domain.Helpers;
using System;
using static PlacowkaOswiatowa.Domain.Helpers.SignalHub;

namespace PlacowkaOswiatowa.Domain.Interfaces.CommonInterfaces
{
    public interface ISignalHub
    {
        event Action LoggedInChanged;
        event Action HideLogingRequest;
        event Action RequestRefreshEmployeesView;
        event Action RequestRefreshStudentsView;
        void RaiseLoggedInChanged();
        void RaiseHideLoginViewRequest();
        public void RaiseRequestRefreshEmployeesView();
        public void RaiseRequestRefreshStudentsView();

        event EventHandler<string> NewMessage;
        void SendMessage(object sender, string message = null);

        event EventHandler<ViewHandler> NewViewRequested;
        void RaiseCreateView(object sender, ViewHandler viewHandler = null);

        void AddAddressCreatedListener(Guid listenerId, AddressCreatedDelegate listener);
        void RaiseAddressCreatedDelegate(Guid listenerId, AdresDto address);
    }
}
