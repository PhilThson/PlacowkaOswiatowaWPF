using AutoMapper;
using PlacowkaOswiatowa.Domain.DTOs;
using PlacowkaOswiatowa.Domain.Exceptions;
using PlacowkaOswiatowa.Domain.Interfaces.RepositoryInterfaces;
using PlacowkaOswiatowa.Domain.Models;
using PlacowkaOswiatowa.Domain.Models.Base;
using PlacowkaOswiatowa.Domain.Resources;
using PlacowkaOswiatowa.ViewModels.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows;

namespace PlacowkaOswiatowa.ViewModels
{
    public class NowyPracownikViewModel : SingleItemViewModel<CreatePracownikDto>
    {
        #region Konstruktor
        public NowyPracownikViewModel(IPlacowkaRepository repository, IMapper mapper)
            : base(repository, mapper, BaseResources.NowyPracownik)
        {
            this.PropertyChanged += (_, __) =>
                _SaveAndCloseCommand.RaiseCanExecuteChanged();
            Item = new CreatePracownikDto { Adres = new AdresDto() };
            //disposing: anulowanie subskrybcji do eventów pochodzących z globalnego zakresu
            // - wykonywane np. w przypadku zamkniecia zkladki
        }
        #endregion

        #region Pola i własności Osoby
        public string Imie
        {
            get => Item.Imie;
            set
            {
                if (value != Item.Imie)
                {
                    Item.Imie = value;
                    base.ClearErrors(nameof(Imie));
                    if (Item.Imie.Length < 3)
                        base.AddError(nameof(Imie), "Imię musi posiadać przynajmniej 3 znaki");
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
                    base.ClearErrors(nameof(DrugieImie));
                    if (Item.DrugieImie.Length != 0 && Item.DrugieImie.Length < 3)
                        base.AddError(nameof(DrugieImie), "Drugie imię musi posiadać przynajmniej 3 znaki");
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
                    base.ClearErrors(nameof(Nazwisko));
                    if (Item.Nazwisko.Length < 3)
                        base.AddError(nameof(Nazwisko), "Nazwisko musi posiadać przynajmniej 3 znaki");
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
                    base.ClearErrors(nameof(Pesel));
                    if (Item.Pesel.Length < 11)
                        base.AddError(nameof(Pesel), "Pesel musi posiadać 11 znaków");
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
                    base.ClearErrors(nameof(DataUrodzenia));
                    if (Item.DataUrodzenia > DateTime.Today)
                        base.AddError(nameof(DataUrodzenia),
                            "Nieprawidłowa data urodzenia");
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
                    ClearErrors(nameof(Panstwo));
                    if (Item.Adres.Panstwo.Length < 1)
                        AddError(nameof(Panstwo), "Należy podać Państwo");
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
                    ClearErrors(nameof(Miejscowosc));
                    if (Item.Adres.Miejscowosc.Length < 1)
                        AddError(nameof(Miejscowosc), "Należy podać miejscowość");
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
                    ClearErrors(nameof(NumerDomu));
                    if (Item.Adres.NumerDomu.Length < 1)
                        AddError(nameof(NumerDomu), "Należy podać numer domu");
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
                    ClearErrors(nameof(KodPocztowy));
                    if (Item.Adres.KodPocztowy.Length < 1)
                        AddError(nameof(KodPocztowy), "Należy podać kod pocztowy");
                    OnPropertyChanged();
                }
            }
        }
        #endregion

        #region Metody komend
        protected override async Task<bool> SaveAsync()
        {
            CheckRequiredProperties();
            if (HasErrors) return false;
            try
            {
                var pracownik = _mapper.Map<Pracownik>(Item);
                
                var pracownikFromDb = await _repository.Pracownicy.GetPracownikByPeselAsync(pracownik.Pesel);
                if (pracownikFromDb != null)
                    throw new DataValidationException("Pracownik o podanym numerze PESEL już istnieje");

                var adres = _mapper.Map<Adres>(Item.Adres);
                var adresFromDb = await _repository.Adresy.GetAdresAsync(adres);
                if (adresFromDb is not null)
                    await _repository.AddEntityAsync(
                        new PracownicyAdresy { AdresId = adresFromDb.Id, Pracownik = pracownik });
                else
                {
                    var properties = adres.GetType().GetProperties()
                        .Where(p => p.PropertyType.BaseType == typeof(BaseDictionaryEntity<int>))
                        .ToList();

                    foreach (var prop in properties)
                    {
                        var toSearch = this.GetType().GetProperty(prop.Name).GetValue(this);
                        MethodInfo method = _repository.GetType().GetMethod("GetByName",
                            BindingFlags.Public | BindingFlags.Instance);
                        MethodInfo genericMethod = method.MakeGenericMethod(prop.PropertyType);
                        var result = genericMethod.Invoke(_repository, new object[] { toSearch });
                        var entity = result as BaseDictionaryEntity<int>;
                        if (entity != null)
                        {
                            prop.SetValue(adres, null);
                            var propId = $"{prop.Name}Id";
                            var propIdInfo = adres.GetType().GetProperty(propId);
                            propIdInfo.SetValue(adres, entity.Id);
                        }
                    }
 
                    pracownik.PracownikPracownicyAdresy = new List<PracownicyAdresy>
                    {
                        new PracownicyAdresy{ Adres = adres }
                    };
                }

                await _repository.Pracownicy.AddAsync(pracownik);
                await _repository.SaveAsync();

                MessageBox.Show("Dodano pracownika!", "Sukces",
                    MessageBoxButton.OK, MessageBoxImage.Information);
                return true;
            }
            catch(DataValidationException e)
            {
                MessageBox.Show(e.Message, "Uwaga",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            catch (Exception e)
            {
                MessageBox.Show($"Nie udało się dodać pracownika. {e.Message}", "Error",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
            return false;
        }

        protected override void ClearForm()
        {
            Item = new CreatePracownikDto() { Adres = new AdresDto() };
            base.ClearAllErrors();
            foreach (var prop in this.GetType().GetProperties())
                this.OnPropertyChanged(prop.Name);
        }

        protected override bool SaveAndCloseCanExecute() => !HasErrors;

        private void CheckRequiredProperties()
        {
            if (string.IsNullOrEmpty(Imie))
            {
                AddError(nameof(Imie), "Należy podać imię");
                OnPropertyChanged(nameof(Imie));
            }
            if (string.IsNullOrEmpty(Nazwisko))
            {
                AddError(nameof(Nazwisko), "Należy podać nazwisko");
                OnPropertyChanged(nameof(Nazwisko));
            }
            if (string.IsNullOrEmpty(DataUrodzenia.ToString()))
            {
                AddError(nameof(DataUrodzenia), "Należy podać datę urodzenia");
                OnPropertyChanged(nameof(DataUrodzenia));
            }
            if (string.IsNullOrEmpty(Pesel))
            {
                AddError(nameof(Pesel), "Należy podać PESEL");
                OnPropertyChanged(nameof(Pesel));
            }
            if (string.IsNullOrEmpty(Panstwo))
            {
                AddError(nameof(Panstwo), "Należy podać państwo");
                OnPropertyChanged(nameof(Panstwo));
            }
            if (string.IsNullOrEmpty(Miejscowosc))
            {
                AddError(nameof(Miejscowosc), "Należy podać miejscowość");
                OnPropertyChanged(nameof(Miejscowosc));
            }
            if (string.IsNullOrEmpty(NumerDomu))
            {
                AddError(nameof(NumerDomu), "Należy podać numer domu");
                OnPropertyChanged(nameof(NumerDomu));
            }
            if (string.IsNullOrEmpty(KodPocztowy))
            {
                AddError(nameof(KodPocztowy), "Należy podać kod pocztowy");
                OnPropertyChanged(nameof(KodPocztowy));
            }
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
