using AutoMapper;
using PlacowkaOswiatowa.Domain.Commands;
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
using System.Windows.Input;

namespace PlacowkaOswiatowa.ViewModels
{
    public class ZarobkiPracownikaViewModel : WorkspaceViewModel, ILoadable
    {
        #region Konstruktor
        public ZarobkiPracownikaViewModel(IPlacowkaRepository repository, IMapper mapper)
            : base(repository)
        {
            base.DisplayName = BaseResources.ZarobkiPracownika;
            _mapper = mapper;
            _pracownikIsVisible = "Collapsed";
            PracownikChanged += WybranyPracownikChanged;
        }
        #endregion

        #region Inicjacja
        [Obsolete("Metoda statyczna do asynchronicznego ładowania ViewModelu")]
        public static async Task<ZarobkiPracownikaViewModel> Load(IPlacowkaRepository repository, IMapper mapper)
        {
            ZarobkiPracownikaViewModel viewModel = new ZarobkiPracownikaViewModel(repository, mapper);
            Task.Run(async () => await viewModel.LoadAsync());
            return viewModel;
        }

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
        //private readonly IPlacowkaRepository _repository;
        private readonly IMapper _mapper;

        private ReadOnlyCollection<PracownikDto> _pracownicy;
        public ReadOnlyCollection<PracownikDto> Pracownicy
        {
            get => _pracownicy;
        }

        private PracownikDto _wybranyPracownik;
        public PracownikDto WybranyPracownik
        {
            get => _wybranyPracownik ??= new PracownikDto();
            set
            {
                if (value != null)
                {
                    _wybranyPracownik = value;
                    OnPropertyChanged(() => WybranyPracownik);
                    RaisePracownikChaged();
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
        public decimal PensjaBrutto
        {
            get => WybranyPracownik.PensjaBrutto;
            set
            {
                if (value != default(decimal))
                {
                    WybranyPracownik.PensjaBrutto = value;
                    OnPropertyChanged(() => PensjaBrutto);
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
                _skladkaZdrowotna = value;
                OnPropertyChanged(() => SkladkaZdrowotna);
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

        #region Komendy
        private BaseCommand _obliczCommand;
        public ICommand ObliczCommand
        {
            get
            {
                if (_obliczCommand == null)
                    _obliczCommand =
                        new BaseCommand(Oblicz);
                return _obliczCommand;
            }
        }

        public ICommand WyczyscCommand => 
            new BaseCommand(WyczyscFormularz);

        #endregion

        #region Metody pomocnicze
        private void Oblicz()
        {
            PensjaNetto = PensjaBrutto - SkladkaChorobowa - SkladkaEmerytalna - SkladkaRentowa - SkladkaZdrowotna;

            //MessageBox.Show($"Obliczono pensję pracownika {Imie} {Nazwisko}", "Info",
            //        MessageBoxButton.OK, MessageBoxImage.Information);
        }
        private void WyczyscFormularz()
        {
            WybranyPracownik = null;
            WymiarGodzinowy = 0;
            Nadgodziny = 0.0;
            SkladkaZdrowotna = 0.0m;
            SkladkaChorobowa = 0.0m;
            SkladkaRentowa = 0.0m;
            SkladkaEmerytalna = 0.0m;
            Clear();
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
            PensjaBrutto = WybranyPracownik.PensjaBrutto;
            SkladkaZdrowotna = PensjaBrutto * 0.09m;
            SkladkaChorobowa = PensjaBrutto * 0.0245m;
            SkladkaRentowa = PensjaBrutto * 0.015m;
            SkladkaEmerytalna = PensjaBrutto * 0.0976m;
        }

        #endregion
    }
}
