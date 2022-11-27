using AutoMapper;
using AutoMapper.Internal;
using PlacowkaOswiatowa.Domain.Commands;
using PlacowkaOswiatowa.Domain.DTOs;
using PlacowkaOswiatowa.Domain.Exceptions;
using PlacowkaOswiatowa.Domain.Interfaces.CommonInterfaces;
using PlacowkaOswiatowa.Domain.Interfaces.RepositoryInterfaces;
using PlacowkaOswiatowa.Domain.Models;
using PlacowkaOswiatowa.Domain.Models.Base;
using PlacowkaOswiatowa.Domain.Resources;
using PlacowkaOswiatowa.ViewModels.Abstract;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace PlacowkaOswiatowa.ViewModels
{
    public class NowyPracownikViewModel : SingleItemViewModel<PracownikDto>, ILoadable
    {
        #region Pola prywatne
        //private Pracownik _pracownik;
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
                    OnPropertyChanged(() => Imie);
                    OnRequestValidate();
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
                    OnRequestValidate();
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
                    OnPropertyChanged(() => DataUrodzenia);
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
                    OnPropertyChanged(() => NrTelefonu);
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
                    OnPropertyChanged(() => Email);
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
                    OnPropertyChanged(() => Panstwo);
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
        //private BaseCommand _dodajPracownikaCommand;
        //public ICommand DodajPracownikaCommand 
        //{ 
        //    get
        //    {
        //        if(_dodajPracownikaCommand == null)
        //            _dodajPracownikaCommand =
        //                new BaseCommand(async () => await DodajPracownika(), 
        //                    () => CzyPrawidlowyPracownik); 
        //        return _dodajPracownikaCommand;
        //        //OnPropertyChanged(() => DodajPracownikaCommand);
        //    }
        //}

        public ICommand WyczyscFormularzCommand => 
            new BaseCommand(WyczyscFormularz);

        //bool CzyPrawidlowy
        protected override bool SaveAndCloseCanExecute() => 
            !string.IsNullOrEmpty(Imie) && Imie.Length >= 3 &&
            !string.IsNullOrEmpty(Nazwisko) && Nazwisko.Length >= 3;

        #endregion

        #region Konstruktor
        public NowyPracownikViewModel(IPlacowkaRepository repository, IMapper mapper)
            : base(BaseResources.NowyPracownik, repository)
        {
            this.RequestValidate += (s, e) =>
                _SaveAndCloseCommand.OnCanExecuteChanged();
            _mapper = mapper;
            Item = new PracownikDto { Adres = new AdresDto(), Etat = new Etat(), Stanowisko = new Stanowisko() };
            //disposing: anulowanie subskrybcji do eventów pochodzących z globalnego zakresu
            // - wykonywane np. w przypadku zamkniecia zkladki
        }
        #endregion

        #region Metody komend
        protected override async Task SaveAsync()
        {
            //DodajPracownikaCommand.CanExecute();
            /*logika dodawania pracownika - ustawienie jego wartosci itd.
            zapis do bazy danych */
            //wyzerowanie parametrów po kliknięciu:
            //this.OnRequestCreateView(this, new EventArgs());
            try
            {
                var pracownik = new Pracownik
                {
                    Imie = Imie,
                    DrugieImie = DrugieImie,
                    Nazwisko = Nazwisko,
                    DataUrodzenia = DataUrodzenia,
                    Pesel = Pesel,
                    //Pensja = Pensja,
                    //WymiarGodzinowy = WymiarGodzinowy,
                    //Nadgodziny = Nadgodziny,
                    NrTelefonu = NrTelefonu,
                    Email = Email,
                    //Stanowisko = WybraneStanowisko,
                    //Etat = WybranyEtat,
                    //DataZatrudnienia = DataZatrudnienia.Value,
                    //DataKoncaZatrudnienia = DataKoncaZatrudnienia
                };

                var adres = new Adres
                {
                    //Panstwo = new Panstwo { Nazwa = Panstwo },
                    //Miejscowosc = new Miejscowosc { Nazwa = Miejscowosc },
                    //Ulica = new Ulica { Nazwa = Ulica },
                    NumerDomu = NumerDomu,
                    NumerMieszkania = NumerMieszkania,
                    KodPocztowy = KodPocztowy
                    //CzyAktywny powinno samo się defaultowo ustawić na true
                };

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

                adres.Panstwo ??= new Panstwo { Nazwa = Panstwo };
                adres.Miejscowosc ??= new Miejscowosc { Nazwa = Miejscowosc };
                adres.Ulica ??= new Ulica { Nazwa = Ulica };

                //var adres = _mapper.Map<AdresDTO, Adres>(_adres);
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
                else
                {
                    //var adresFromDb = await _repository.Adresy.GetAsync(a => a.Ulica == _adres.Ulica &&
                    //    a.Miejscowosc == _adres.Miejscowosc &&
                    //    a.NumerDomu == _adres.NumerDomu &&
                    //    a.NumerMieszkania == _adres.NumerMieszkania &&
                    //    a.KodPocztowy == _adres.KodPocztowy);
                    //AdresId = adresFromDb.Id;
                    var adresFromDb = await _repository.Adresy.GetAsync(a => a == adres,
                        includeProperties: "Panstwo,Miejscowosc,Ulica");

                    if (adresFromDb is null)
                        throw new DataNotFoundException("Nie znaleziono adresu o podanych parametrach");

                    pracownik.PracownikPracownicyAdresy = new List<PracownicyAdresy>
                    {
                        new PracownicyAdresy{ AdresId = adresFromDb.Id }
                    };
                }

               
                    //(Imie, DrugieImie, Nazwisko, DataUrodzenia, Pesel, 
                    //AdresId, _adres, Pensja, null, WymiarGodzinowy, Nadgodziny, NrTelefonu,
                    //Email, Etat, Stanowisko, DataZatrudnienia, DataKoncaZatrudnienia, null);

                //var pracownik = _mapper.Map<PracownikDTO, Pracownik>(_pracownik);
                var czyIstnieje = await _repository.Pracownicy.Exists(pracownik);
                if (!czyIstnieje)
                {
                    await _repository.Pracownicy.AddAsync(pracownik);
                    await _repository.SaveAsync();

                    MessageBox.Show("Dodano pracownika!", "Sukces",
                    MessageBoxButton.OK, MessageBoxImage.Information);
                }
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
            Imie = "";
            DrugieImie = "";
            Nazwisko = "";
            DataUrodzenia = DateTime.Today;
            Pesel = "";
            Ulica = "";
            NumerDomu = "";
            NumerMieszkania = "";
            Miejscowosc = "";
            Panstwo = "";
            KodPocztowy = "";
            //DataZatrudnienia = DateTime.Today;
            //DataKoncaZatrudnienia = DateTime.Today;
            //WybranyEtat = null;
            //WybraneStanowisko = null;
            //Pensja = 0;
            Email = "";
            NrTelefonu = "";
            //Nadgodziny = 0;
            //WymiarGodzinowy = 0;
        }
        #endregion

        #region Obsługa zdarzeń
        private void CheckForCanExecute(object? sender, EventArgs e) =>
            _SaveAndCloseCommand.OnCanExecuteChanged();

        [Obsolete("Funkcja do walidacji przy zmianie właściwości ViewModelu")]
        private void NowyPracownikViewModel_PropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(Imie) ||
                e.PropertyName == nameof(Nazwisko))
            {
                //nie trzeba subskrybować eventu CanExecuteChanged
                //bo automatycznie robi to funkcja CanExecute() w BaseCommand
                _SaveAndCloseCommand.OnCanExecuteChanged();
            }
        }

        public override void Dispose()
        {
            //potrzebne w razie subskrybowania eventu z innej klasy
            //której czas życia trwa przez cały okres trwania aplikacji
            //potencjalne wycieki pamięci
            //this.PropertyChanged -= NowyPracownikViewModel_PropertyChanged;
            base.Dispose();
        }

        #endregion

        #region Pobranie zasobów z bazy danych
        public async Task LoadAsync()
        {
            try
            {
                //var etatyFromDb = await _repository.Etaty.GetAllAsync();
                //_etaty = new ReadOnlyCollection<Etat>(etatyFromDb);
                //OnPropertyChanged(() => Etaty);

                //var stanowiskaFromDb = await _repository.Stanowiska.GetAllAsync();
                //_stanowiska = new ReadOnlyCollection<Stanowisko>(stanowiskaFromDb);
                //OnPropertyChanged(() => Stanowiska);
            }
            catch (Exception e)
            {
                MessageBox.Show("Nie udało się załadować danych.", "Error",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        #endregion
    }
}
