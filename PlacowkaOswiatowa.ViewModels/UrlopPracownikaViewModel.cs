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
    public class UrlopPracownikaViewModel : SingleItemViewModel<UrlopPracownikaDto>, ILoadable
    {
        #region Konstruktor
        public UrlopPracownikaViewModel(IPlacowkaRepository repository, IMapper mapper)
            : base(BaseResources.UrlopPracownika, repository, mapper)
        {
            WybranyPracownik = new PracownikDto();
            _pracownikIsVisible = "Collapsed";
            _isEnabled = false;
            this.PropertyChanged += (_, __) =>
                _SaveAndCloseCommand.RaiseCanExecuteChanged();
        }
        #endregion

        #region Inicjacja

        public async Task LoadAsync()
        {
            try
            {
                var pracownicyFormDb = await _repository.Pracownicy.GetAllAsync();
                var pracownicy = _mapper.Map<List<PracownikDto>>(pracownicyFormDb);

                _pracownicy = new ReadOnlyCollection<PracownikDto>(pracownicy);
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
                    OnZastepujacyPracownikChanged();
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
                        IsEnabled = IsEnabled != true;
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

        #region Komendy

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
            IsEnabled && string.IsNullOrEmpty(Zastepca);

        #endregion

        #region Metody pomocnicze
        protected override async Task SaveAsync()
        {
            //Wpis do tabeli urlopów
            MessageBox.Show($"Poprawnie dodano urlop pracownikowa", "Info",
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
            base.Clear();
        }

        private void OnZastepujacyPracownikChanged()
        {
            base.Clear();
            if (ZastepujacyPracownik?.Id == WybranyPracownik?.Id)
            {
                AddValidationMessage("Niepoprawne dane",
                                   "Pracownik nie może zastępować samego siebie!");
            }
        }
        #endregion
    }
}
