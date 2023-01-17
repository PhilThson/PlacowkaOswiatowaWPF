using AutoMapper;
using PlacowkaOswiatowa.Domain.DTOs;
using PlacowkaOswiatowa.Domain.Interfaces.CommonInterfaces;
using PlacowkaOswiatowa.Domain.Interfaces.RepositoryInterfaces;
using PlacowkaOswiatowa.Domain.Resources;
using PlacowkaOswiatowa.ViewModels.Abstract;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;

namespace PlacowkaOswiatowa.ViewModels
{
    public class ZarobkiPracownikaViewModel : SingleItemViewModel<Skladki>, ILoadable
    {

        #region Konstruktor
        public ZarobkiPracownikaViewModel(IPlacowkaRepository repository, IMapper mapper)
            : base(repository, mapper, BaseResources.ZarobkiPracownika)
        {
            _pracownikIsVisible = "Collapsed";
            _skladki = new Skladki();
            _wybranyPracownik = new PracownikDto();
        }
        #endregion

        #region Inicjacja

        public async Task LoadAsync()
        {
            try
            {
                var pracownicyFormDb = await _repository.Pracownicy.GetAllAsync();
                var listaPracownikow = _mapper.Map<List<PracownikDto>>(pracownicyFormDb);
                _pracownicy = new ReadOnlyCollection<PracownikDto>(listaPracownikow);
                OnPropertyChanged(() => Pracownicy);
            }
            catch (Exception e)
            {
                MessageBox.Show("Nie udało się załadować danych.", "Error",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        #endregion

        #region Pola, właściwości

        private Skladki _skladki;
        public Skladki Skladki 
        {
            get => _skladki;
            set => SetProperty(ref _skladki, value);
        }


        private ReadOnlyCollection<PracownikDto> _pracownicy;
        public ReadOnlyCollection<PracownikDto> Pracownicy
        {
            get => _pracownicy;
        }

        private PracownikDto _wybranyPracownik;
        public PracownikDto WybranyPracownik
        {
            get => _wybranyPracownik;
            set
            {
                if (value != null)
                {
                    _wybranyPracownik = value;
                    OnPropertyChanged(() => WybranyPracownik);
                    WybranyPracownikChanged();
                    PracownikIsVisible = "Visible";
                }
            }
        }

        private string _pracownikIsVisible;
        public string PracownikIsVisible 
        {
            get => _pracownikIsVisible;
            set
            {
                if(value != string.Empty)
                {
                    _pracownikIsVisible = value;
                    OnPropertyChanged(() => PracownikIsVisible);
                }
            }
        }

        #endregion

        #region Dane pracownika
        public int PracownikId
        {
            get => WybranyPracownik.Id;
            set 
            { 
                if(value != default(int))
                {
                    WybranyPracownik.Id = value;
                    OnPropertyChanged(() => PracownikId);
                }
            }
        }
        public string Imie
        {
            get => WybranyPracownik.Imie;
            set
            {
                if (value != null)
                {
                    WybranyPracownik.Imie = value;
                    OnPropertyChanged(() => Imie);
                }
            }
        }
        public string Nazwisko
        {
            get => WybranyPracownik.Nazwisko;
            set
            {
                if (value != null)
                {
                    WybranyPracownik.Nazwisko = value;
                    OnPropertyChanged(() => Nazwisko);
                }
            }
        }
        public string Pesel
        {
            get => WybranyPracownik.Pesel;
            set
            {
                if (value != null)
                {
                    WybranyPracownik.Pesel = value;
                    OnPropertyChanged(() => Pesel);
                }
            }
        }
        public DateTime? DataUrodzenia
        {
            get => WybranyPracownik.DataUrodzenia;
            set
            {
                if (value != null)
                {
                    WybranyPracownik.DataUrodzenia = value;
                    OnPropertyChanged(() => DataUrodzenia);
                }
            }
        }
        private decimal _pensjaNetto;
        public decimal PensjaNetto
        {
            get => _pensjaNetto;
            set
            {
                _pensjaNetto = value;
                OnPropertyChanged(() => PensjaNetto);
            }
        }
        public decimal WynagrodzenieBrutto
        {
            get => WybranyPracownik.WynagrodzenieBrutto;
            set
            {
                if (value != default(decimal))
                {
                    WybranyPracownik.WynagrodzenieBrutto = value;
                    OnPropertyChanged(() => WynagrodzenieBrutto);
                }
            }
        }
        public double? WymiarGodzinowy
        {
            get => WybranyPracownik.WymiarGodzinowy;
            set
            {
                if (value != null)
                {
                    WybranyPracownik.WymiarGodzinowy = value;
                    OnPropertyChanged(() => WymiarGodzinowy);
                }
            }
        }
        public double? Nadgodziny
        {
            get => WybranyPracownik.Nadgodziny;
            set
            {
                if (value != null)
                {
                    WybranyPracownik.Nadgodziny = value;
                    OnPropertyChanged(() => Nadgodziny);
                }
            }
        }
        private decimal _skladkaZdrowotna;
        public decimal SkladkaZdrowotna
        {
            get => _skladkaZdrowotna;
            set
            {
                if (value != null)
                {
                    _skladkaZdrowotna = value;
                    OnPropertyChanged(() => SkladkaZdrowotna);
                }
            }
        }
        private decimal _skladkaChorobowa;
        public decimal SkladkaChorobowa
        {
            get => _skladkaChorobowa;
            set
            {
                _skladkaChorobowa = value;
                OnPropertyChanged(() => SkladkaChorobowa);
            }
        }
        private decimal _skladkaRentowa;
        public decimal SkladkaRentowa
        {
            get => _skladkaRentowa;
            set
            {
                _skladkaRentowa = value;
                OnPropertyChanged(() => SkladkaRentowa);
            }
        }
        private decimal _skladkaEmerytalna;
        public decimal SkladkaEmerytalna
        {
            get => _skladkaEmerytalna;
            set
            {
                _skladkaEmerytalna = value;
                OnPropertyChanged(() => SkladkaEmerytalna);
            }
        }

        #endregion

        #region Metody pomocnicze
        private void Oblicz()
        {
            

            MessageBox.Show($"Obliczono pensję pracownika {Imie} {Nazwisko}", "Info",
                    MessageBoxButton.OK, MessageBoxImage.Information);
        }
        protected override void ClearForm()
        {
            WybranyPracownik = new PracownikDto();
            Skladki = new Skladki();
            PensjaNetto = 0.0m;
            WymiarGodzinowy = 0;
            Nadgodziny = 0.0;
            SkladkaZdrowotna = 0.0m;
            SkladkaChorobowa = 0.0m;
            SkladkaRentowa = 0.0m;
            SkladkaEmerytalna = 0.0m;
        }

        private void WybranyPracownikChanged()
        {
            PracownikId = WybranyPracownik.Id;
            Imie = WybranyPracownik.Imie;
            Nazwisko = WybranyPracownik.Nazwisko;
            DataUrodzenia = WybranyPracownik.DataUrodzenia;
            Pesel = WybranyPracownik.Pesel;
            WymiarGodzinowy = WybranyPracownik.WymiarGodzinowy;
            Nadgodziny = WybranyPracownik.Nadgodziny;
            WynagrodzenieBrutto = WybranyPracownik.WynagrodzenieBrutto;
            SkladkaZdrowotna = WynagrodzenieBrutto * 0.09m;
            SkladkaChorobowa = WynagrodzenieBrutto * 0.0245m;
            SkladkaRentowa = WynagrodzenieBrutto * 0.015m;
            SkladkaEmerytalna = WynagrodzenieBrutto * 0.0976m;
        }

        protected override Task<bool> SaveAsync()
        {
            Oblicz();

            return Task.FromResult(false);
        }

        #endregion
    }

    public class Skladki
    {
        #region Pola prywatne
        private decimal _skladkaEmerytalna;
        private decimal _skladkaRentowa;
        private decimal _skladkaChorobowa;
        private decimal _skladkaZdrowotna;
        private decimal _podatek;
        private decimal _zaliczkaNaPodatekDochodowy;
        #endregion

        #region Właściwości

        #region Składki społeczne
        public decimal SkladkaEmerytalnaProcent { get; set; }
        public decimal SkladkaRenotwaProcent { get; set; }
        public decimal SkladkaChorobowaProcent { get; set; }
        #endregion
        //obliczna z pensji brutto po odjeciu skladek społecznych
        public decimal SkladkaZdrowotnaProcent { get; set; }
        //po odjęciu poprzednich od kwoty brutto, odjęcie kosztów uzyskania przychodu
        //co daje zaliczkę na podatek dochodowy
        public decimal KosztUzyskaniaPrzychodu { get; set; } //PLN
        //kwota pozostała po odjęciu powyższych w I progu podatkowym to 12%
        //zwolnieni od podatku - 0%
        public decimal PodatekProcent { get; set; }
        //Właściwie 1/12 kwoty wolnej od podatku czyli 300 PLN
        public decimal KwotaWolnaOdPodatku { get; set; }
        #endregion

        public Skladki()
        {
            SkladkaEmerytalnaProcent = 0.0976m;
            SkladkaRenotwaProcent = 0.015m;
            SkladkaChorobowaProcent = 0.0245m;
            SkladkaZdrowotnaProcent = 0.09m;
            KosztUzyskaniaPrzychodu = 250.0m;
            PodatekProcent = 0.12m;
            KwotaWolnaOdPodatku = 300.0m;
        }

        public decimal ObliczNetto(decimal kwotaBrutto)
        {
            _skladkaEmerytalna = kwotaBrutto * SkladkaEmerytalnaProcent;
            _skladkaRentowa = kwotaBrutto * SkladkaRenotwaProcent;
            _skladkaChorobowa = kwotaBrutto * SkladkaChorobowaProcent;
            var skladkiSpoleczne = _skladkaEmerytalna + _skladkaRentowa + _skladkaChorobowa;
            _skladkaZdrowotna = (kwotaBrutto - skladkiSpoleczne) * SkladkaZdrowotnaProcent;

            //Wynagrodzenie pomniejszone o składki społeczne
            var kwotaBezSkladek = kwotaBrutto - skladkiSpoleczne - _skladkaZdrowotna;
            //kwotaBrutto -= _skladkaZdrowotna;
            //Wyliczenie kwoty do opodatkowania
            var kwotaDoOpodatkowania = Math.Round(kwotaBezSkladek - KosztUzyskaniaPrzychodu, 2);
            _podatek = kwotaDoOpodatkowania * PodatekProcent;
            //W przypadku osób zwolnionych od podatku, nie jest brane pod uwagę
            _zaliczkaNaPodatekDochodowy = _podatek - KwotaWolnaOdPodatku;
            var kwotaNetto = kwotaBrutto - skladkiSpoleczne - _skladkaZdrowotna - _zaliczkaNaPodatekDochodowy;
            return kwotaNetto;
        }
    }
}
