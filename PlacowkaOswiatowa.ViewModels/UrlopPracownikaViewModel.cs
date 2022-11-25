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
    public class UrlopPracownikaViewModel : WorkspaceViewModel, ILoadable
    {
        #region Konstruktor
        public UrlopPracownikaViewModel(IPlacowkaRepository repository, IMapper mapper)
            : base(repository)
        {
            base.DisplayName = BaseResources.UrlopPracownika;
            _mapper = mapper;
            _pracownikIsVisible = "Collapsed";
            PracownikChanged += OnWybranyPracownikChanged;
            RequestValidate += OnZastepujacyPracownikChanged;
            _isEnabled = false;

        }
        #endregion

        #region Inicjacja
        [Obsolete("Metoda statyczna do asynchronicznego ładowania ViewModelu")]
        public static async Task<UrlopPracownikaViewModel> Load(IPlacowkaRepository repository, IMapper mapper)
        {
            UrlopPracownikaViewModel viewModel = new UrlopPracownikaViewModel(repository, mapper);
            Task.Run(async () => await viewModel.LoadAsync());
            return viewModel;
        }

        public async Task LoadAsync()
        {
            try
            {
                var pracownicyFormDb = await _repository.Pracownicy.GetAllAsync();
                var listaWychowawcow = _mapper.Map<List<PracownikDto>>(pracownicyFormDb);

                _pracownicy = new ReadOnlyCollection<PracownikDto>(listaWychowawcow);
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
            get => _wybranyPracownik;
            set
            {
                _wybranyPracownik = value;
                OnPropertyChanged(() => WybranyPracownik);
                if (value != null)
                {
                    RaisePracownikChaged();
                    PracownikIsVisible = "Visible";
                }
            }
        }

        private PracownikDto? _zastepujacyPracownik;
        public PracownikDto? ZastepujacyPracownik
        {
            get => _zastepujacyPracownik;
            set
            {
                _zastepujacyPracownik = value;
                OnPropertyChanged(() => ZastepujacyPracownik);
                if (value != null)
                {
                    OnRequestValidate();
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

        private ObservableCollection<FlagViewModel> _flagsViewModel;
        public ObservableCollection<FlagViewModel> FlagsViewModel
        {
            get
            {
                if (_flagsViewModel == null)
                {
                    var mP = new FlagViewModel(false, "Czy pracownik z listy?");
                    var flags = new List<FlagViewModel>();
                    flags.Add(mP);
                    _flagsViewModel = new ObservableCollection<FlagViewModel>(flags);
                    _flagsViewModel[0].PropertyChanged += (s, e) =>
                        IsEnabled = IsEnabled == true ? false : true;
                }
                return _flagsViewModel;
            }
            set 
            {
                _flagsViewModel = value;
                OnPropertyChanged(() => FlagsViewModel);
            }
        }

        private bool _isEnabled;
        public bool IsEnabled
        {
            get => _isEnabled;
            set
            {
                _isEnabled = value;
                 OnPropertyChanged(() => IsEnabled);
            }
        }

        public bool UmowaIsEnabled => false;

        private string _zastepca;
        public string Zastepca
        {
            get => _zastepca;
            set 
            { 
                if(value != null)
                {
                    _zastepca = value;
                    OnPropertyChanged(() => Zastepca);
                }

            }
        }


        #endregion

        #region Dane pracownika
        private int _pracownikId;
        public int PracownikId
        {
            get { return _pracownikId; }
            set 
            { 
                _pracownikId = value;
                OnPropertyChanged(() => PracownikId);
            }
        }

        private string _imie;
        public string Imie
        {
            get { return _imie; }
            set
            {
                if (value != null)
                {
                    _imie = value;
                    OnPropertyChanged(() => Imie);
                }
            }
        }

        private string _nazwisko;
        public string Nazwisko
        {
            get { return _nazwisko; }
            set
            {
                if (value != null)
                {
                    _nazwisko = value;
                    OnPropertyChanged(() => Nazwisko);
                }
            }
        }

        private string _pesel;
        public string Pesel
        {
            get { return _pesel; }
            set
            {
                if (value != null)
                {
                    _pesel = value;
                    OnPropertyChanged(() => Pesel);
                }
            }
        }

        private string _dataUrodzenia;
        public string DataUrodzenia
        {
            get => _dataUrodzenia;
            set
            {
                if (value != null)
                {
                    _dataUrodzenia = value;
                    OnPropertyChanged(() => DataUrodzenia);
                }
            }
        }
        private int _dniUrlopu;
        public int DniUrlopu
        {
            get => _dniUrlopu;
            set 
            { 
                _dniUrlopu = value;
                OnPropertyChanged(() => DniUrlopu);
            }
        }
        #endregion

        #region Komendy
        private BaseCommand _dodajUrlopCommand;
        public ICommand DodajUrlopCommand
        {
            get
            {
                if (_dodajUrlopCommand == null)
                    _dodajUrlopCommand =
                        new BaseCommand(DodajUrlop);
                return _dodajUrlopCommand;
            }
        }

        private BaseCommand _wyczyscCommand;
        public ICommand WyczyscCommand
        {
            get
            {
                if (_wyczyscCommand == null)
                    _wyczyscCommand = new BaseCommand(WyczyscFormularz);
                return _wyczyscCommand;
            }
        }

        public bool CzyPrawidlowy =>
            !string.IsNullOrEmpty(Imie) && Imie.Length >= 3 &&
            !string.IsNullOrEmpty(Nazwisko) && Nazwisko.Length >= 3;

        #endregion

        #region Metody pomocnicze
        private void DodajUrlop()
        {
            MessageBox.Show($"Poprawnie dodano urlop pracownikowi {Imie} {Nazwisko}", "Info",
                    MessageBoxButton.OK, MessageBoxImage.Information);
        }
        private void WyczyscFormularz()
        {
            ZastepujacyPracownik = null;
            WybranyPracownik = null;
            IsEnabled = false;
            FlagsViewModel = null;
            PracownikIsVisible = "Collapsed";
            Zastepca = "";
            Clear();
        }

        private void OnWybranyPracownikChanged()
        {
            PracownikId = WybranyPracownik.Id;
            Imie = WybranyPracownik.Imie;
            Nazwisko = WybranyPracownik.Nazwisko;
            DataUrodzenia = WybranyPracownik.DataUrodzenia.ToString();
            DniUrlopu = WybranyPracownik.DniUrlopu;
        }

        private void OnZastepujacyPracownikChanged(object? sender, EventArgs e)
        {
            //Clear();
            if (ZastepujacyPracownik?.Id == WybranyPracownik?.Id)
            {
                AddValidationMessage("Niepoprawne dane",
                                   "Pracownik nie może zastępować samego siebie!");
            }
        }
        #endregion
    }
}
