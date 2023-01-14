using PlacowkaOswiatowa.Domain.Commands;
using PlacowkaOswiatowa.Domain.Helpers;
using PlacowkaOswiatowa.Domain.Interfaces.RepositoryInterfaces;
using PlacowkaOswiatowa.Domain.MessageBroker;
using PlacowkaOswiatowa.Domain.Models;
using PlacowkaOswiatowa.Domain.Resources;
using PlacowkaOswiatowa.ViewModels.Abstract;
using System;
using System.Windows;
using System.Windows.Input;

namespace PlacowkaOswiatowa.ViewModels
{
    public class LoginViewModel : WorkspaceViewModel
    {
        #region Konstruktor
        public LoginViewModel(IPlacowkaRepository repository, ISignalHub signal)
            : base(repository)
        {
            _signal = signal;
            DisplayName = BaseResources.LoginPage;
            DisplayStatusMessage("Logowanie do aplikacji");
            this.PropertyChanged += (s, e) => _zalogujCommand.OnCanExecuteChanged();
            _login = "Gość";
        }
        #endregion

        #region Pola i Właściwości

        private readonly ISignalHub _signal;

        private Uzytkownik _uzytkownik;

        private string _login;
        public string Login
        {
            get => _login;
            set => SetProperty(ref _login, value);
        }

        private string _password;
        public string Password
        {
            get => _password;
            set => SetProperty(ref _password, value);
        }

        public bool CzyPoprawne =>
            !string.IsNullOrEmpty(Login) && Login.Length >= 3 &&
            !string.IsNullOrEmpty(Password) && Password.Length >= 3;
        #endregion

        #region Komendy
        private BaseCommand _zalogujCommand;
        public ICommand ZalogujCommand
        {
            get
            {
                if (_zalogujCommand == null)
                    _zalogujCommand =
                        new BaseCommand(() => Zaloguj(), () => CzyPoprawne);
                return _zalogujCommand;
            }
        }

        public ICommand AnulujCommand
        {
            get => new BaseCommand(() => this.Close(false));
        }
        #endregion
       
        #region Logowanie
        public async void Zaloguj()
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
                Close(false);
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
                        Close(false);
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
                    PublishException(ex);
                    MessageBox.Show("Błąd połączenia do bazy danych", "Error",
                        MessageBoxButton.OK, MessageBoxImage.Error);
                    _signal.WyslijWiadomosc(this, "Błąd połączenia do bazy danych.");
                }
            }
        }
        #endregion

        #region Metody pomocnicze

        public override void Close(bool wasCancelled = true)
        {
            if (wasCancelled)
            {
                MessageBroker.Instance.SendMessage(
                    MessageBrokerMessages.DISPLAY_TIMEOUT_INFO_MESSAGE_TITLE,
                       "User NOT Logged In.");
            }

            base.Close(wasCancelled);
            base.Clear();
            Login = "Gość";
            Password = String.Empty;
            _signal.RaiseHideLoginViewRequest();
        }

        #endregion
    }
}
