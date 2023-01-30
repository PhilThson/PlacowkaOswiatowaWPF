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

namespace PlacowkaOswiatowa.ViewModels
{
    public class RejestracjaViewModel : SingleItemViewModel<CreateUzytkownikDto>,
        ILoadable
    {
        #region Konstruktor
        public RejestracjaViewModel(IServiceProvider serviceProvider, IMapper mapper)
            : base(serviceProvider, mapper, BaseResources.Rejestracja)
        {
            Item = new CreateUzytkownikDto();
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

                    if(uzytkownikFromDb != null)
                        throw new DataValidationException(
                            "Wybrany adres e-mail jest już zajęty");

                    await repository.AddAsync(uzytkownik);
                    await repository.SaveAsync();
                }

                MessageBox.Show("Poprawnie utworzono użytkownika", "Sukces",
                    MessageBoxButton.OK, MessageBoxImage.Information);

                return true;
            }
            catch (DataValidationException e)
            {
                MessageBox.Show(e.Message, "Uwaga",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            catch (Exception e)
            {
                MessageBox.Show($"Nie udało się zapisać użytkownika. {e.Message}", 
                    "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            return false;
        }

        public async Task LoadAsync()
        {
            try
            {
                var role = new List<Rola>();
                using (var scope = _serviceProvider.CreateScope())
                {
                    var repository = scope.ServiceProvider.GetRequiredService<IPlacowkaRepository>();
                    role = await repository.Role.GetAllAsync();
                }
                Role = new ObservableCollection<Rola>(role);
            }
            catch (Exception e)
            {
                MessageBox.Show($"Nie udało się pobrać ról. {e.Message}", "Error",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
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
