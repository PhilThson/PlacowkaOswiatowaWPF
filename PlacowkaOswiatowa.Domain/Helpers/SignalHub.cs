using PlacowkaOswiatowa.Domain.DTOs;
using System;

namespace PlacowkaOswiatowa.Domain.Helpers
{
    public class SignalHub : ISignalHub
    {
        private static SignalHub _Instance;
        public static SignalHub Instance
        {
            get => _Instance ??= new SignalHub();
        }

        private SignalHub()
        {

        }

        public event Action LoggedInChanged;
        public void RaiseLoggedInChanged() => LoggedInChanged?.Invoke();

        public event Action HideLogingRequest;
        public void RaiseHideLoginViewRequest() => HideLogingRequest?.Invoke();


        public event EventHandler<string> NewMessage;
        public void SendMessage(object sender, string message = null) =>
            RaiseNewMessage(sender, message);
            
        private void RaiseNewMessage(object sender, string message) =>
            NewMessage?.Invoke(sender, message);


        public event EventHandler<ViewHandler> NewViewRequested;
        public void RaiseCreateView(object sender, ViewHandler obj = null) =>
            NewViewRequested?.Invoke(sender, obj);


        public event EventHandler<AdresDto> AddressCreated;
        public void RaiseAddressCreated(object sender, AdresDto address) =>
            AddressCreated?.Invoke(sender, address);
    }

    public interface ISignalHub
    {
        event Action LoggedInChanged;
        event Action HideLogingRequest;
        void RaiseLoggedInChanged();
        void RaiseHideLoginViewRequest();

        /// <summary>
        /// Zdarzenie do obsługi rozgłaszania wiadomości
        /// </summary>
        event EventHandler<string> NewMessage;
        void SendMessage(object sender, string message = null);

        public event EventHandler<ViewHandler> NewViewRequested;
        public void RaiseCreateView(object sender, ViewHandler viewHandler = null);

        public event EventHandler<AdresDto> AddressCreated;
        public void RaiseAddressCreated(object sender, AdresDto address);
    }
}
