using System;

namespace PlacowkaOswiatowa.Domain.Helpers
{
    public class SignalHub : ISignalHub
    {
        public event Action LoggedInChanged;
        public void RaiseLoggedInChanged() => LoggedInChanged?.Invoke();

        public event Action HideLogingRequest;
        public void RaiseHideLoginViewRequest() => HideLogingRequest?.Invoke();

        public event EventHandler<string> NowaWiadomosc;
        public void WyslijWiadomosc(object wysylacz, string wiadomosc)
        {
            RozglosWiadomosc(wysylacz, wiadomosc);
        }
            
        private void RozglosWiadomosc(object sender, string message)
        {
            NowaWiadomosc?.Invoke(sender, message);
        }
    }

    public interface ISignalHub
    {
        event Action LoggedInChanged;
        event Action HideLogingRequest;
        event EventHandler<string> NowaWiadomosc;
        void WyslijWiadomosc(object wysylacz, string wiadomosc);
        void RaiseLoggedInChanged();
        void RaiseHideLoginViewRequest();
    }
}
