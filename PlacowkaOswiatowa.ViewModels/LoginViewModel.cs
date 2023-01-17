using AutoMapper;
using PlacowkaOswiatowa.Domain.Commands;
using PlacowkaOswiatowa.Domain.DTOs;
using PlacowkaOswiatowa.Domain.Helpers;
using PlacowkaOswiatowa.Domain.Interfaces.RepositoryInterfaces;
using PlacowkaOswiatowa.Domain.Models;
using PlacowkaOswiatowa.Domain.Resources;
using PlacowkaOswiatowa.ViewModels.Abstract;
using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace PlacowkaOswiatowa.ViewModels
{
    public class LoginViewModel : SingleItemViewModel<UzytkownikDto>
    {
        #region Konstruktor
        public LoginViewModel(ISignalHub signal, 
            IPlacowkaRepository repository, 
            IMapper mapper)
            : base(repository, mapper, BaseResources.LoginPage)
        {
            _signal = signal;
            //DisplayStatusMessage("Logowanie do aplikacji");
            this.PropertyChanged += (s, e) => 
                _SaveAndCloseCommand.RaiseCanExecuteChanged();
            //_login = "Gość";
            Item = new UzytkownikDto();
        }
        #endregion

        #region Pola i Właściwości

        private readonly ISignalHub _signal;

        private string _login;
        public string Login
        {
            get => _login;
            set
            {
                if(value != Item.Login)
                {
                    _login = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _password;
        public string Password
        {
            get => _password;
            set
            {
                if(value != Item.Password)
                {
                    _password = value;
                    OnPropertyChanged();
                }
            }
        }

        #endregion

        #region Komendy
        public ICommand AnulujCommand
        {
            get => new BaseCommand(Close);
        }
        #endregion
       
        #region Logowanie
        protected override async Task<bool> SaveAsync()
        {
            var uzytkownik = new Uzytkownik
            {
                Email = _login,
                HashHasla = _password
            };
            if (uzytkownik.Email == "Gość" && uzytkownik.HashHasla == "1234")
            {
                _signal.RaiseLoggedInChanged();
                _signal.WyslijWiadomosc(this, "Witaj Gościu!");
                Close();
            }
            else
            {
                try
                {
                    bool czyJestPracownikiem = await _repository.Pracownicy.UserExists(uzytkownik);

                    if (czyJestPracownikiem == true)
                    {
                        _signal.RaiseLoggedInChanged();
                        _signal.WyslijWiadomosc(this, $"Witaj {Login} {Password}!");
                        Close();
                    }
                    else
                    {
                        AddValidationMessage("LoginFailed",
                                        "Niepoprawny login i/lub hasło.");

                        _signal.WyslijWiadomosc(this, "Nie udało się zalogować.");
                    }
                }
                catch (Exception ex)
                {
                    //PublishException(ex);
                    MessageBox.Show("Błąd połączenia do bazy danych", "Error",
                        MessageBoxButton.OK, MessageBoxImage.Error);
                    _signal.WyslijWiadomosc(this, "Błąd połączenia do bazy danych.");
                }
            }
            return false;
        }
        #endregion

        #region Metody pomocnicze

        public void Close()
        {
            ClearValidationMessages();
            Item = new UzytkownikDto();
            foreach (var prop in this.GetType().GetProperties())
                OnPropertyChanged(prop.Name);

            _signal.RaiseHideLoginViewRequest();
        }

        protected override bool SaveAndCloseCanExecute() =>
            !string.IsNullOrEmpty(Login) && Login.Length >= 3 &&
            !string.IsNullOrEmpty(Password) && Password.Length >= 3;

        protected override void ClearForm() => Close();
        
        #endregion
    }
}
