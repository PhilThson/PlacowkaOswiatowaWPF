using AutoMapper;
using PlacowkaOswiatowa.Domain.Commands;
using PlacowkaOswiatowa.Domain.DTOs;
using PlacowkaOswiatowa.Domain.Exceptions;
using PlacowkaOswiatowa.Domain.Interfaces.RepositoryInterfaces;
using PlacowkaOswiatowa.Domain.Models;
using PlacowkaOswiatowa.Domain.Models.Base;
using PlacowkaOswiatowa.Domain.Resources;
using PlacowkaOswiatowa.ViewModels.Abstract;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace PlacowkaOswiatowa.ViewModels
{
    public class NowyPracownikViewModel : SingleItemViewModel<CreatePracownikDto>
    {
        #region Pola i własności Osoby
        public string Imie
        {
            get => Item.Imie;
            set
            {
                if (value != Item.Imie)
                {
                    Item.Imie = value;
                    OnPropertyChanged();
                }
            }
        }
        public string DrugieImie
        {
            get => Item.DrugieImie;
            set 
            {
                if (value != Item.DrugieImie)
                {
                    Item.DrugieImie = value;
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
                    OnPropertyChanged();
                }
            }
        }
        public string Pesel
        {
            get => Item.Pesel;
            set
            {
                if (value != Item.Pesel)
                {
                    Item.Pesel = value;
                    OnPropertyChanged();
                }
            }
        }
        public DateTime? DataUrodzenia
        {
            get => Item.DataUrodzenia;
            set
            {
                if (value != Item.DataUrodzenia)
                {
                    Item.DataUrodzenia = value;
                    OnPropertyChanged();
                }
            }
        }
        #endregion

        #region Pola i właściwości Pracownika
        public string NrTelefonu
        {
            get => Item.NrTelefonu;
            set
            {
                if (value != Item.NrTelefonu)
                {
                    Item.NrTelefonu = value;
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
                    OnPropertyChanged();
                }
            }
        }

        #endregion

        #region Pola i własności Adresu
        //public int? AdresId { get; set; }
        public string Panstwo
        {
            get => Item.Adres.Panstwo;
            set
            {
                if (value != Item.Adres.Panstwo)
                {
                    Item.Adres.Panstwo = value;
                    OnPropertyChanged();
                }
            }
        }
        public string Miejscowosc
        {
            get => Item.Adres.Miejscowosc;
            set
            {
                if (value != Item.Adres.Miejscowosc)
                {
                    Item.Adres.Miejscowosc = value;
                    OnPropertyChanged();
                }
            }
        }
        public string Ulica
        {
            get => Item.Adres.Ulica;
            set
            {
                if (value != Item.Adres.Ulica)
                {
                    Item.Adres.Ulica = value;
                    OnPropertyChanged();
                }
            }
        }
        public string NumerDomu
        {
            get => Item.Adres.NumerDomu;
            set
            {
                if (value != Item.Adres.NumerDomu)
                {
                    Item.Adres.NumerDomu = value;
                    OnPropertyChanged();
                }
            }
        }
        public string NumerMieszkania
        {
            get => Item.Adres.NumerMieszkania;
            set
            {
                if (value != Item.Adres.NumerMieszkania)
                {
                    Item.Adres.NumerMieszkania = value;
                    OnPropertyChanged();
                }
            }
        }
        public string KodPocztowy
        {
            get => Item.Adres.KodPocztowy;
            set
            {
                if (value != Item.Adres.KodPocztowy)
                {
                    Item.Adres.KodPocztowy = value;
                    OnPropertyChanged();
                }
            }
        }
        #endregion

        #region Komendy

        public ICommand WyczyscFormularzCommand => 
            new BaseCommand(WyczyscFormularz);

        protected override bool SaveAndCloseCanExecute() => 
            !string.IsNullOrEmpty(Imie) && Imie.Length >= 3 &&
            !string.IsNullOrEmpty(Nazwisko) && Nazwisko.Length >= 3;

        #endregion

        #region Konstruktor
        public NowyPracownikViewModel(IPlacowkaRepository repository, IMapper mapper)
            : base(BaseResources.NowyPracownik, repository, mapper)
        {
            this.PropertyChanged += (_, __) =>
                _SaveAndCloseCommand.RaiseCanExecuteChanged();
            Item = new CreatePracownikDto { Adres = new AdresDto() };
            //disposing: anulowanie subskrybcji do eventów pochodzących z globalnego zakresu
            // - wykonywane np. w przypadku zamkniecia zkladki
        }
        #endregion

        #region Metody komend
        protected override async Task SaveAsync()
        {
            try
            {
                var pracownik = _mapper.Map<Pracownik>(Item);
                var adres = _mapper.Map<Adres>(Item.Adres);

                //Mapowanie właściwości posiadających klucz obcy do adresu
                //jeżeli dana właściwość juz istnieje w bazie danych,
                //to zostanie przypisany klucz obcy
                var properties = adres.GetType().GetProperties()
                    .Where(p => p.PropertyType.BaseType == typeof(BaseDictionaryEntity<int>))
                    .ToList();
                foreach(var prop in properties)
                {
                    var toSearch = this.GetType().GetProperty(prop.Name).GetValue(this);
                    MethodInfo method = _repository.GetType().GetMethod("GetByName",
                        BindingFlags.Public | BindingFlags.Instance);
                    MethodInfo genericMethod = method.MakeGenericMethod(prop.PropertyType);
                    var result = genericMethod.Invoke(_repository, new object[] { toSearch });
                    var entity = result as BaseDictionaryEntity<int>;
                    if (entity != null)
                        prop.SetValue(adres, entity);
                }

                //jeżeli nie to zostanie utworzony nowy rekord
                //to zrobi automaper
                adres.Panstwo ??= new Panstwo { Nazwa = Panstwo };
                adres.Miejscowosc ??= new Miejscowosc { Nazwa = Miejscowosc };
                adres.Ulica ??= new Ulica { Nazwa = Ulica };

                //jeżeli adres nie istnieje to dodanie do bazy
                var czyAdresIstnieje = await _repository.Adresy.Exists(adres);
                if (!czyAdresIstnieje)
                {
                    await _repository.Adresy.AddAsync(adres);
                    await _repository.SaveAsync();
                    
                    pracownik.PracownikPracownicyAdresy = new List<PracownicyAdresy>
                    {
                        new PracownicyAdresy{ AdresId = adres.Id }
                    };
                }
                //jeżeli istnieje to przypisanie istniejącego identyfikatora
                else
                {
                    var adresFromDb = await _repository.Adresy.GetAsync(a => a == adres);

                    if (adresFromDb is null)
                        throw new DataNotFoundException("Nie znaleziono adresu o podanych parametrach");

                    pracownik.PracownikPracownicyAdresy = new List<PracownicyAdresy>
                    {
                        new PracownicyAdresy{ AdresId = adresFromDb.Id }
                    };
                }

                //jeżeli pracownik nie istnieje to dodanie
                var czyIstnieje = await _repository.Pracownicy.Exists(pracownik);
                if (!czyIstnieje)
                {
                    await _repository.Pracownicy.AddAsync(pracownik);
                    await _repository.SaveAsync();

                    MessageBox.Show("Dodano pracownika!", "Sukces",
                    MessageBoxButton.OK, MessageBoxImage.Information);
                }
                //w przeciwnym wypadku dodanie do bazy
                else
                {
                    MessageBox.Show("Pracownik już istnieje.", "Uwaga",
                    MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Nie udało się dodać pracownika", "Error",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
            //podnieść Event rozgłaszając dodanie pracownika,
            //bo może być otwarta zakładka z listą pracowników,
            //która się nie odświeży
        }

        public void WyczyscFormularz()
        {
            Item = new CreatePracownikDto() { Adres = new AdresDto() };
            base.ClearAllErrors();
            foreach (var prop in this.GetType().GetProperties())
                this.OnPropertyChanged(prop.Name);
        }
        #endregion

        #region Obsługa zdarzeń
        
        public override void Dispose()
        {
            //potrzebne w razie subskrybowania eventu z innej klasy
            //której czas życia trwa przez cały okres trwania aplikacji
            //potencjalne wycieki pamięci
            //this.PropertyChanged -= NowyPracownikViewModel_PropertyChanged;
            base.Dispose();
        }

        #endregion
    }
}
