using System;

namespace PlacowkaOswiatowa.Domain.Helpers
{
    public class SignalHub<T> : ISignalHub<T> 
        where T : class
    {
        private static SignalHub<T> _Instance;
        public static SignalHub<T> Instance
        {
            get => _Instance ??= new SignalHub<T>();
        }

        private SignalHub()
        {

        }

        public event Action LoggedInChanged;
        public void RaiseLoggedInChanged() => LoggedInChanged?.Invoke();

        public event Action HideLogingRequest;
        public void RaiseHideLoginViewRequest() => HideLogingRequest?.Invoke();

        public event EventHandler<T> NewMessage;
        public void SendMessage(object sender, T message = null) =>
            RaiseNewMessage(sender, message);
            
        private void RaiseNewMessage(object sender, T message) =>
            NewMessage?.Invoke(sender, message);

        public event EventHandler<T> NewViewRequested;

        public void RaiseCreateView(object sender, T obj = null) =>
            NewViewRequested?.Invoke(sender, obj);
    }

    public interface ISignalHub<T> where T : class
    {
        event Action LoggedInChanged;
        event Action HideLogingRequest;
        /// <summary>
        /// Zdarzenie do obsługi rozgłaszania wiadomości
        /// </summary>
        event EventHandler<T> NewMessage;
        void SendMessage(object sender, T message = null);
        void RaiseLoggedInChanged();
        void RaiseHideLoginViewRequest();

        public event EventHandler<T> NewViewRequested;
        public void RaiseCreateView(object sender, T obj = null);
    }
}
