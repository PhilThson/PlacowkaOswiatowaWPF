using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using PlacowkaOswiatowa.Domain.Commands;
using PlacowkaOswiatowa.Domain.DTOs;
using PlacowkaOswiatowa.Domain.Exceptions;
using PlacowkaOswiatowa.Domain.Helpers;
using PlacowkaOswiatowa.Domain.Interfaces.CommonInterfaces;
using PlacowkaOswiatowa.Domain.Interfaces.RepositoryInterfaces;
using PlacowkaOswiatowa.Domain.Models;
using PlacowkaOswiatowa.Domain.Resources;
using PlacowkaOswiatowa.ViewModels.Abstract;
using PlacowkaOswiatowa.ViewModels.Helpers;
using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace PlacowkaOswiatowa.ViewModels
{
    public class LoginViewModel : SingleItemViewModel<UzytkownikDto>
    {
        #region Pola prywatne
        private readonly ISignalHub _signal;
        #endregion

        #region Konstruktor
        public LoginViewModel(IServiceProvider serviceProvider, IMapper mapper)
            : base(serviceProvider, mapper, BaseResources.LoginPage)
        {
            _signal = SignalHub.Instance;
            //DisplayStatusMessage("Logowanie do aplikacji");
            this.PropertyChanged += (s, e) => 
                _SaveAndCloseCommand.RaiseCanExecuteChanged();
            //_login = "Gość";
            Item = new UzytkownikDto();
        }
        #endregion

        #region Właściwości

        public string Email
        {
            get => Item.Email;
            set
            {
                if(value != Item.Email)
                {
                    Item.Email = value;
                    OnPropertyChanged();
                }
            }
        }
        public string Password
        {
            get => Item.Password;
            set
            {
                if(value != Item.Password)
                {
                    Item.Password = value;
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

        private ICommand _RegisterCommand;
        public ICommand RegisterCommand => _RegisterCommand ??=
            new BaseCommand(Register);

        private void Register()
        {
            var viewHandler = new ViewHandler(typeof(RejestracjaViewModel), isModal: true);
            _signal.RaiseCreateView(this, viewHandler);
        }
        #endregion

        #region Logowanie
        protected override async Task<bool> SaveAsync()
        {
            if (Item.Email == "Gość" && Item.Password == "1234")
            {
                _signal.RaiseLoggedInChanged();
                _signal.SendMessage(this, "Witaj Gościu!");
                Close();
                return true;
            }
            try
            {
                Uzytkownik uzytkownik = null;
                using (var provider = _serviceProvider.CreateScope())
                {
                    var repository = provider.ServiceProvider.GetRequiredService<IPlacowkaRepository>();
                    uzytkownik = await repository.Uzytkownicy.GetAsync(u => u.Email == Item.Email);
                }

                if (uzytkownik is null)
                    throw new DataValidationException("Nie znaleziono użytkownika o podanym adesie e-mail");

                if (!SecurePasswordHasher.Verify(Item.Password, uzytkownik.HashHasla))
                    throw new DataValidationException("Niepoprawne hasło.");

                _signal.RaiseLoggedInChanged();
                _signal.SendMessage(this, $"Witaj {uzytkownik.Imie} {uzytkownik.Nazwisko}!");
                Close();
                return true;
            }
            catch(DataValidationException e)
            {
                AddValidationMessage("Błąd", "Niepoprawny e-mail i/lub hasło.");
                _signal.SendMessage(this, "Nie udało się zalogować.");
            }
            catch (Exception e)
            {
                MessageBox.Show($"Błąd. {e.Message}", "Error",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                _signal.SendMessage(this, "Błąd podczas logowania.");
            }

            return false;
        }
        #endregion

        #region Metody pomocnicze

        public void Close()
        {
            ClearValidationMessages();
            Item = new UzytkownikDto();
            OnPropertyChanged(Email);
            OnPropertyChanged(Password);

            _signal.RaiseHideLoginViewRequest();
        }

        protected override bool SaveAndCloseCanExecute() =>
            !string.IsNullOrEmpty(Email) && Email.Length >= 3 &&
            !string.IsNullOrEmpty(Password) && Password.Length >= 3;

        protected override void ClearForm(object _) => Close();
        
        #endregion
    }
}
