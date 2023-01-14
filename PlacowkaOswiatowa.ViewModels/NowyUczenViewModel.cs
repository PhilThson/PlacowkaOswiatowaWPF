using PlacowkaOswiatowa.Domain.Interfaces.RepositoryInterfaces;
using PlacowkaOswiatowa.Domain.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using AutoMapper;
using PlacowkaOswiatowa.Domain.Interfaces.CommonInterfaces;
using PlacowkaOswiatowa.Domain.Commands;
using PlacowkaOswiatowa.Domain.Resources;
using PlacowkaOswiatowa.ViewModels.Abstract;
using PlacowkaOswiatowa.Domain.DTOs;
using PlacowkaOswiatowa.Domain.Exceptions;
using System.ComponentModel;
using PlacowkaOswiatowa.ViewModels.Helpers;
using System.Windows.Threading;
using System.Collections;

namespace PlacowkaOswiatowa.ViewModels
{
    public class NowyUczenViewModel : SingleItemViewModel<UczenDto>, ILoadable, INotifyDataErrorInfo
    {
        #region Prywatne pola
        //private Uczen _uczen;
        //private Adres _adres;
        //private readonly IPlacowkaRepository _repository;
        private readonly IMapper _mapper;
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

                    _errorsViewModel.ClearErrors(nameof(Imie));
                    if (Item.Imie.Length > 2)
                        _errorsViewModel.AddError(nameof(Imie), $"{nameof(Imie)} może posiadać maksymalnie 20 znaków");

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
                    _errorsViewModel.ClearErrors(nameof(DataUrodzenia));
                    if (Item.DataUrodzenia > DateTime.Today)
                        _errorsViewModel.AddError(nameof(DataUrodzenia),
                            "Nieprawidłowa data urodzenia");

                    OnPropertyChanged(() => DataUrodzenia);
                }
            }
        }
        #endregion

        #region Pola i właściwości Ucznia

        private ReadOnlyCollection<OddzialDto> _oddzialy;
        public ReadOnlyCollection<OddzialDto> Oddzialy => _oddzialy;

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
                    OnPropertyChanged(() => KodPocztowy);
                }
            }
        }
        #endregion

        #region Komendy

        public ICommand WyczyscFormularzCommand 
        { 
            get => new BaseCommand(WyczyscFormularz);
        }
        #endregion

        #region Obsługa błędów

        private readonly ErrorsViewModel _errorsViewModel;

        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;
        public bool HasErrors => _errorsViewModel.HasErrors;
        public bool CanSave => !_errorsViewModel.HasErrors;

        public IEnumerable GetErrors(string propertyName) =>
            _errorsViewModel.GetErrors(propertyName);

        #endregion

        #region Konstruktor
        public NowyUczenViewModel(IPlacowkaRepository repository, IMapper mapper)
            : base(BaseResources.NowyUczen, repository)
        {
            Item = new UczenDto { Adres = new AdresDto() };
            //Wyłączenie przycisku dopóki formularz nie przejdzie walidacji
            this.PropertyChanged += (s, e) =>
                _SaveAndCloseCommand.OnCanExecuteChanged();
            _mapper = mapper;
            _errorsViewModel = new ErrorsViewModel();
            _errorsViewModel.ErrorsChanged += ErrorsViewModel_ErrorsChanged;
        }

        private void ErrorsViewModel_ErrorsChanged(object sender, DataErrorsChangedEventArgs e)
        {
            ErrorsChanged?.Invoke(sender, e);
            OnPropertyChanged(nameof(CanSave));
        }
        #endregion

        #region Pobieranie danych z bazy

        public async Task LoadAsync()
        {
            try
            {
                var grupyFromDb = await _repository.Oddzialy.GetAllAsync(includeProperties: "Pracownik");
                var listaOddzialow = _mapper.Map<List<OddzialDto>>(grupyFromDb);

                _oddzialy = new ReadOnlyCollection<OddzialDto>(listaOddzialow);
                OnPropertyChanged(() => Oddzialy);
            }
            catch (Exception e)
            {
                MessageBox.Show("Nie udało się załadować oddziałów.", "Error",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        #endregion

        #region Metody

        protected override async Task SaveAsync()
        {
            try
            {
                //wyzerowanie parametrów po kliknięciu:
                //_pracownik.ClearPracownik();
                //OnPropertyChanged(() => _pracownik.Imie);
                //this.OnRequestCreateView(this, new EventArgs());

                var uczen = _mapper.Map<Uczen>(Item);
                uczen.OddzialId = WybranyOddzial.Id;

                var adres = _mapper.Map<Adres>(Item.Adres);

                var czyAdresIstnieje = await _repository.Adresy.Exists(adres);
                if (!czyAdresIstnieje)
                {
                    await _repository.Adresy.AddAsync(adres);
                    await _repository.SaveAsync();
                    //bo adresDTO jest śledzony
                    //AdresId = _adres.Id;
                    uczen.AdresId = adres.Id;
                }
                else
                {
                    var adresFromDb = await _repository.Adresy.GetAsync(a => a == adres);

                    if (adresFromDb is null)
                        throw new DataNotFoundException("Nie znaleziono adresu o podanych parametrach");

                    uczen.AdresId = adresFromDb.Id;
                }

                var czyIstnieje = await _repository.Uczniowie.Exists(uczen);

                if(!czyIstnieje)
                {
                    await _repository.Uczniowie.AddAsync(uczen);
                    await _repository.SaveAsync();

                    MessageBox.Show("Dodano ucznia!", "Sukces",
                        MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show("Uczeń już istnieje.", "Uwaga",
                        MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Nie udało się dodać ucznia", "Błąd",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
            //podnieść Event rozgłaszając dodanie ucznia,
            //bo może być otwarta zakładka z listą pracowników,
            //która się nie odświeży
        }

        public void WyczyscFormularz()
        {
            //Imie = "";
            //DrugieImie = "";
            //Nazwisko = "";
            //DataUrodzenia = DateTime.Today;
            //Pesel = "";
            //Ulica = "";
            //NumerDomu = "";
            //NumerMieszkania = "";
            //Miejscowosc = "";
            //Panstwo = "";
            //KodPocztowy = "";
            //WybranyOddzial = null;
            Item = new UczenDto { Adres = new AdresDto() };
            foreach(var prop in this.GetType().GetProperties())
                this.OnPropertyChanged(prop.Name);
        }

        protected override bool SaveAndCloseCanExecute() =>
            !string.IsNullOrEmpty(Imie) && Imie.Length >= 3 &&
            !string.IsNullOrEmpty(Nazwisko) && Nazwisko.Length >= 3 &&
            DataUrodzenia.HasValue &&
            DataUrodzenia.Value < DateTime.Now.AddYears(-3);

        #endregion

        #region Obsługa zdarzeń
        private void CheckForCanExecute(object? sender, EventArgs e) =>
            _SaveAndCloseCommand.OnCanExecuteChanged();
        #endregion

        public override void Dispose()
        {
            //potrzebne w razie subskrybowania eventu z innej klasy
            //której czas życia trwa przez cały okres trwania aplikacji
            //potencjalne wycieki pamięci
            //this.PropertyChanged -= NowyUczenViewModel_PropertyChanged;
            base.Dispose();
        }
    }
}
