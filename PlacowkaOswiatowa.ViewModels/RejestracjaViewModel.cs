using Microsoft.Extensions.DependencyInjection;
using PlacowkaOswiatowa.Domain.DTOs;
using PlacowkaOswiatowa.Domain.Interfaces.CommonInterfaces;
using PlacowkaOswiatowa.Domain.Interfaces.RepositoryInterfaces;
using PlacowkaOswiatowa.Domain.Models;
using PlacowkaOswiatowa.ViewModels.Abstract;
using System.Collections.Generic;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;
using PlacowkaOswiatowa.Domain.Exceptions;
using PlacowkaOswiatowa.ViewModels.Helpers;
using AutoMapper;
using PlacowkaOswiatowa.Domain.Resources;
using Microsoft.Extensions.Logging;

namespace PlacowkaOswiatowa.ViewModels
{
    public class RejestracjaViewModel : SingleItemViewModel<CreateUzytkownikDto>,
        ILoadable
    {
        #region Pola prywatne
        private readonly ILogger<RejestracjaViewModel> _logger;
        #endregion

        #region Konstruktor
        public RejestracjaViewModel(IServiceProvider serviceProvider, IMapper mapper,
            ILogger<RejestracjaViewModel> logger)
            : base(serviceProvider, mapper, BaseResources.Rejestracja)
        {
            Item = new CreateUzytkownikDto();
            _logger = logger;
        }
        #endregion

        #region Właściwości użytkownika
        public string Imie
        {
            get => Item.Imie;
            set
            {
                if (value != Item.Imie)
                {
                    Item.Imie = value;
                    ClearErrors(nameof(Imie));
                    if (Item.Imie.Length < 3)
                        AddError(nameof(Imie), "Imię musi posiadać przynajmniej 3 znaki");

                    OnPropertyChanged();
                }
            }
        }
        public string Nazwisko
        {
            get => Item.Nazwisko;
            set
            {
                if (value != Item.Nazwisko)
                {
                    Item.Nazwisko = value;
                    ClearErrors(nameof(Nazwisko));
                    if (Item.Nazwisko.Length < 3)
                        AddError(nameof(Nazwisko), "Nazwisko musi posiadać przynajmniej 3 znaki");

                    OnPropertyChanged();
                }
            }
        }
        public string Email
        {
            get => Item.Email;
            set
            {
                if (value != Item.Email)
                {
                    Item.Email = value;
                    ClearErrors(nameof(Email));
                    if (Item.Email.Length < 3)
                        AddError(nameof(Nazwisko), "E-mail musi posiadać przynajmniej 3 znaki");
                    OnPropertyChanged();
                }
            }
        }
        public string Haslo
        {
            get => Item.Haslo;
            set
            {
                if (value != Item.Haslo)
                {
                    Item.Haslo = value;
                    ClearErrors(nameof(Haslo));
                    ClearValidationMessages();
                    if (Item.Nazwisko.Length < 3)
                        AddError(nameof(Haslo), "Hasło musi posiadać przynajmniej 3 znaki");

                    OnPropertyChanged();
                }
            }
        }
        public string PowtorzHaslo
        {
            get => Item.PowtorzHaslo;
            set
            {
                if (value != Item.PowtorzHaslo)
                {
                    Item.PowtorzHaslo = value;
                    ClearErrors(nameof(PowtorzHaslo));
                    ClearValidationMessages();
                    if (Item.PowtorzHaslo.Length < 3)
                        AddError(nameof(PowtorzHaslo), "Nazwisko musi posiadać przynajmniej 3 znaki");

                    OnPropertyChanged();
                }
            }
        }
        #endregion

        #region Lista ról
        private ObservableCollection<Rola> _Role;
        public ObservableCollection<Rola> Role
        {
            get => _Role;
            set => SetProperty(ref _Role, value);
        }

        public Rola Rola
        {
            get => Item.Rola;
            set
            {
                if (value != Item.Rola)
                {
                    Item.Rola = value;
                    ClearErrors(nameof(Rola));
                    OnPropertyChanged();
                }
            }
        }
        #endregion

        #region Metody
        protected override async Task<bool> SaveAsync()
        {
            try
            {
                Item.HashHasla = SecurePasswordHasher.Hash(Haslo);

                var uzytkownik = _mapper.Map<Uzytkownik>(Item);

                using (var scope = _serviceProvider.CreateScope())
                {
                    var repository = scope.ServiceProvider.GetRequiredService<IPlacowkaRepository>();

                    var uzytkownikFromDb =
                        await repository.Uzytkownicy.GetAsync(u => u.Email == uzytkownik.Email);

                    if (uzytkownikFromDb != null)
                        throw new DataValidationException(
                            "Wybrany adres e-mail jest już zajęty");

                    await repository.AddAsync(uzytkownik);
                    await repository.SaveAsync();
                }

                MessageBox.Show("Poprawnie utworzono użytkownika", "Sukces",
                    MessageBoxButton.OK, MessageBoxImage.Information);

                _logger.LogInformation(
                    "Utworzono nowego użytkownika: {imie} {nazwisko}, {email}",
                    uzytkownik.Imie, uzytkownik.Nazwisko, uzytkownik.Email);

                return true;
            }
            catch (DataValidationException e)
            {
                MessageBox.Show(e.Message, "Uwaga",
                    MessageBoxButton.OK, MessageBoxImage.Warning);

                _logger.LogWarning("Nieudana próba utworzenia użytkownika: {error}", e.Message);
            }
            catch (Exception e)
            {
                MessageBox.Show($"Nie udało się zapisać użytkownika. {e.Message}",
                    "Error", MessageBoxButton.OK, MessageBoxImage.Error);

                _logger.LogError("Błąd podczas tworzenia nowego użytkownika: {error}", e.Message);
            }
            return false;
        }

        public async Task LoadAsync()
        {
            var role = new List<Rola>();
            using (var scope = _serviceProvider.CreateScope())
            {
                var repository = scope.ServiceProvider.GetRequiredService<IPlacowkaRepository>();
                role = await repository.Role.GetAllAsync();
            }
            Role = new ObservableCollection<Rola>(role);
        }

        protected override void CheckRequiredProperties()
        {
            BaseCheckRequiredProperties(
                nameof(Imie),
                nameof(Nazwisko),
                nameof(Email),
                nameof(Haslo),
                nameof(PowtorzHaslo),
                nameof(Rola));

            if (Haslo != PowtorzHaslo)
                AddValidationMessage(nameof(PowtorzHaslo), "Brak zgodności haseł");
        }

        #endregion
    }
}
