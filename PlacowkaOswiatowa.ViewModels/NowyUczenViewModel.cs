using PlacowkaOswiatowa.Domain.Interfaces.RepositoryInterfaces;
using PlacowkaOswiatowa.Domain.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;
using AutoMapper;
using PlacowkaOswiatowa.Domain.Interfaces.CommonInterfaces;
using PlacowkaOswiatowa.Domain.Resources;
using PlacowkaOswiatowa.ViewModels.Abstract;
using PlacowkaOswiatowa.Domain.DTOs;
using PlacowkaOswiatowa.Domain.Exceptions;
using PlacowkaOswiatowa.Domain.Models.Base;
using System.Reflection;
using System.Linq;

namespace PlacowkaOswiatowa.ViewModels
{
    public class NowyUczenViewModel : SingleItemViewModel<UczenDto>, ILoadable, IEditable
    {
        #region Konstruktor
        public NowyUczenViewModel(IPlacowkaRepository repository, IMapper mapper)
            : base(repository, mapper, BaseResources.NowyUczen)
        {
            Item = new UczenDto { Adres = new AdresDto() };
            //Wyłączenie przycisku dopóki formularz nie przejdzie walidacji
            this.ErrorsChanged += (s, e) =>
                _SaveAndCloseCommand.RaiseCanExecuteChanged();
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

                    OnPropertyChanged(() => Imie);
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

                    OnPropertyChanged(() => DrugieImie);
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

                    OnPropertyChanged(() => Nazwisko);
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
                    OnPropertyChanged(() => Pesel);
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

                    OnPropertyChanged(() => DataUrodzenia);
                }
            }
        }
        #endregion

        #region Pola i właściwości Ucznia

        private ReadOnlyCollection<OddzialDto> _oddzialy;
        public ReadOnlyCollection<OddzialDto> Oddzialy
        {
            get => _oddzialy;
        }

        public OddzialDto WybranyOddzial
        {
            get => Item.Oddzial;
            set
            {
                if(value != null && value != Item.Oddzial)
                {
                    Item.Oddzial = value;
                    OnPropertyChanged(() => WybranyOddzial);
                }
            }
        }

        #endregion

        #region Pola i własności Adresu
        public string Panstwo
        {
            get => Item.Adres.Panstwo;
            set 
            { 
                if(value != Item.Adres.Panstwo)
                {
                    Item.Adres.Panstwo = value;

                    ClearErrors(nameof(Panstwo));
                    if (Item.Adres.Panstwo.Length < 1)
                        AddError(nameof(Panstwo), "Należy podać Państwo");

                    OnPropertyChanged(() => Panstwo);
                }
            }
        }
        public string Miejscowosc
        {
            get => Item.Adres.Miejscowosc;
            set 
            { 
                if(value != Item.Adres.Miejscowosc)
                {
                    Item.Adres.Miejscowosc = value;

                    ClearErrors(nameof(Miejscowosc));
                    if (Item.Adres.Miejscowosc.Length < 1)
                        AddError(nameof(Miejscowosc), "Należy podać miejscowość");

                    OnPropertyChanged(() => Miejscowosc);
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
                    OnPropertyChanged(() => Ulica);
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
                    OnPropertyChanged(() => NumerDomu);
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
                    OnPropertyChanged(() => NumerMieszkania);
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
                    OnPropertyChanged(() => KodPocztowy);
                }
            }
        }
        #endregion

        #region Metody

        protected override async Task<bool> SaveAsync()
        {
            CheckRequiredProperties();
            if (HasErrors) return false;
            try
            {
                var uczen = _mapper.Map<Uczen>(Item);
                if (uczen.OddzialId != default)
                    uczen.Oddzial = null;

                var uczenFromDb = await _repository.Uczniowie.GetUczenByPesel(uczen.Pesel);
                if (uczenFromDb != null)
                    throw new DataValidationException("Uczeń o podanym nr PESEL już istnieje");
                
                var adres = _mapper.Map<Adres>(Item.Adres);

                var adresFromDb = await _repository.Adresy.GetAdresAsync(adres);
                if (adresFromDb is not null)
                    uczen.AdresId = adresFromDb.Id;
                else
                {
                    uczen.Adres = adres;
                    //Dla powiązanych encji sprawdź czy istnieje rekord o zadanej nazwie,
                    //jeżeli tak, to pobierz istniejący, jeżeli nie, to utwórz nowy
                    var properties = uczen.Adres.GetType().GetProperties()
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
                            prop.SetValue(uczen.Adres, null);
                            var propId = $"{prop.Name}Id";
                            var propIdInfo = uczen.Adres.GetType().GetProperty(propId);
                            propIdInfo.SetValue(uczen.Adres, entity.Id);
                        }
                    }
                }

                await _repository.Uczniowie.AddAsync(uczen);
                await _repository.SaveAsync();

                MessageBox.Show("Dodano ucznia!", "Sukces",
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
                MessageBox.Show($"Nie udało się dodać ucznia. {e.Message}", 
                    "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            return false;
        }

        protected override void ClearForm()
        {
            Item = new UczenDto { Adres = new AdresDto() };
            base.ClearAllErrors();
            foreach(var prop in this.GetType().GetProperties())
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

        #region Inicjacja

        public async Task LoadAsync()
        {
            try
            {
                var oddzialyFromDb = await _repository.Oddzialy.GetAllAsync(includeProperties: "Pracownik");
                var listaOddzialow = _mapper.Map<List<OddzialDto>>(oddzialyFromDb);

                _oddzialy = new ReadOnlyCollection<OddzialDto>(listaOddzialow);
                OnPropertyChanged(() => Oddzialy);
            }
            catch (Exception e)
            {
                MessageBox.Show("Nie udało się załadować oddziałów.", "Error",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public async Task LoadItem(object objId)
        {
            try
            {
                var uczenFromDb = await _repository.Uczniowie.GetByIdAsync((int)objId) ??
                    throw new DataNotFoundException(
                        $"Nie znaleziono ucznia o podanym identyfikatorze ({objId})");

                base.DisplayName = BaseResources.EdycjaUcznia;
                base.AddItemName = BaseResources.SaveItem;

                Item = _mapper.Map<UczenDto>(uczenFromDb);
                Item.Adres ??= new AdresDto();
                foreach (var prop in this.GetType().GetProperties())
                    this.OnPropertyChanged(prop.Name);
            }
            catch (Exception e)
            {
                MessageBox.Show($"Nie udało się pobrać danych ucznia. {e.Message}",
                    "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        #endregion

        #region Helpers
        public override void Dispose()
        {
            //potrzebne w razie subskrybowania eventu z innej klasy
            //której czas życia trwa przez cały okres trwania aplikacji
            //potencjalne wycieki pamięci
            //this.PropertyChanged -= NowyUczenViewModel_PropertyChanged;
            base.Dispose();
        }
        #endregion
    }
}
