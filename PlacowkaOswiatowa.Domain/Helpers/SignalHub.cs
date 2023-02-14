﻿using PlacowkaOswiatowa.Domain.DTOs;
using PlacowkaOswiatowa.Domain.Interfaces.CommonInterfaces;
using System;
using System.Collections.Generic;

namespace PlacowkaOswiatowa.Domain.Helpers
{
    public class SignalHub : ISignalHub
    {
        #region Pola prywatne
        //zabezpieczenie przed równoległym dostępem i utworzeniem kilku instancji
        private static readonly Lazy<SignalHub> lazy
            = new Lazy<SignalHub>(() => new SignalHub());

        private Dictionary<Guid, AddressCreatedDelegate> _addressCreatedListeners =
            new Dictionary<Guid, AddressCreatedDelegate>();
        #endregion

        #region Konstruktor
        private SignalHub()
        {

        }
        #endregion

        #region Właściwości
        public static SignalHub Instance => lazy.Value;
        #endregion

        #region Zdarzenia

        public event Action LoggedInChanged;
        public event Action HideLogingRequest;
        public event Action RequestRefreshEmployeesView;
        public event Action RequestRefreshStudentsView;
        public event EventHandler<string> NewMessage;
        public event EventHandler<ViewHandler> NewViewRequested;

        public delegate void AddressCreatedDelegate(AdresDto address, Guid listenerId);

        #endregion

        #region Metody

        public void RaiseLoggedInChanged() => LoggedInChanged?.Invoke();
                
        public void RaiseHideLoginViewRequest() => HideLogingRequest?.Invoke();
        
        public void RaiseRequestRefreshEmployeesView() =>
            RequestRefreshEmployeesView?.Invoke();
        
        public void RaiseRequestRefreshStudentsView() =>
            RequestRefreshStudentsView?.Invoke();
        
        public void SendMessage(object sender, string message = null) =>
            RaiseNewMessage(sender, message);
            
        private void RaiseNewMessage(object sender, string message) =>
            NewMessage?.Invoke(sender, message);
                
        public void RaiseCreateView(object sender, ViewHandler obj = null) =>
            NewViewRequested?.Invoke(sender, obj);


        public void AddAddressCreatedListener(Guid listenerId, AddressCreatedDelegate listener)
        {
            if (_addressCreatedListeners.ContainsKey(listenerId))
                return;

            _addressCreatedListeners.Add(listenerId, listener);
        }

        public void RaiseAddressCreatedDelegate(Guid listenerId, AdresDto address)
        {
            if (!_addressCreatedListeners.TryGetValue(listenerId, out AddressCreatedDelegate listener))
                return;

            listener?.Invoke(address, listenerId);
            _addressCreatedListeners.Remove(listenerId);
        }

        #endregion
    }
}
