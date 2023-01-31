using PlacowkaOswiatowa.Domain.DTOs;
using System;

namespace PlacowkaOswiatowa.Domain.Helpers
{
    public class SignalHub : ISignalHub
    {
        //próba zabezpieczenia przed równoległym dostępem i utworzenia kilku instancji
        private static readonly Lazy<SignalHub> lazy
            = new Lazy<SignalHub>(() => new SignalHub());

        public static SignalHub Instance => lazy.Value;

        private SignalHub()
        {

        }

        public event Action LoggedInChanged;
        public void RaiseLoggedInChanged() => LoggedInChanged?.Invoke();

        public event Action HideLogingRequest;
        public void RaiseHideLoginViewRequest() => HideLogingRequest?.Invoke();

        public event Action RequestRefreshEmployeesView;
        public void RaiseRequestRefreshEmployeesView() =>
            RequestRefreshEmployeesView?.Invoke();

        public event Action RequestRefreshStudentsView;
        public void RaiseRequestRefreshStudentsView() =>
            RequestRefreshStudentsView?.Invoke();


        public event EventHandler<string> NewMessage;
        public void SendMessage(object sender, string message = null) =>
            RaiseNewMessage(sender, message);
            
        private void RaiseNewMessage(object sender, string message) =>
            NewMessage?.Invoke(sender, message);


        public event EventHandler<ViewHandler> NewViewRequested;
        public void RaiseCreateView(object sender, ViewHandler obj = null) =>
            NewViewRequested?.Invoke(sender, obj);

        public delegate void AddressCreatedDelegate(AdresDto address);

        public static AddressCreatedDelegate AddressCreated;

        public static void RaiseAddressCreatedDelegate(AdresDto address)
        {
            AddressCreated?.Invoke(address);
            AddressCreated = null;
        }
    }

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

        /// <summary>
        /// Zdarzenie do obsługi rozgłaszania wiadomości
        /// </summary>
        event EventHandler<string> NewMessage;
        void SendMessage(object sender, string message = null);

        public event EventHandler<ViewHandler> NewViewRequested;
        public void RaiseCreateView(object sender, ViewHandler viewHandler = null);
    }
}
