using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using PlacowkaOswiatowa.Domain.DTOs;
using PlacowkaOswiatowa.Domain.Interfaces.CommonInterfaces;
using PlacowkaOswiatowa.Domain.Interfaces.RepositoryInterfaces;
using PlacowkaOswiatowa.Domain.Models;
using PlacowkaOswiatowa.Domain.Resources;
using PlacowkaOswiatowa.ViewModels.Abstract;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;

namespace PlacowkaOswiatowa.ViewModels
{
    public class UrlopPracownikaViewModel : SingleItemViewModel<UrlopPracownikaDto>, ILoadable
    {
        #region Konstruktor
        public UrlopPracownikaViewModel(IServiceProvider serviceProvider, IMapper mapper)
            : base(serviceProvider, mapper, BaseResources.UrlopPracownika)
        {
            Item = new UrlopPracownikaDto 
            { 
                Pracownik = new PracownikDto(),
                //{ Stanowisko = new StanowiskoDto},

                PoczatekUrlopu = DateTime.Today,
                KoniecUrlopu = DateTime.Today
            };
            _pracownikIsVisible = "Collapsed";
            _isEnabled = false;
            //Przycisk zapisu będzie dostępny jeżeli nie będzie błędów
            //this.PropertyChanged += (_, __) =>
            //    _SaveAndCloseCommand.RaiseCanExecuteChanged();
        }
        #endregion

        #region Inicjalizacja

        public async Task LoadAsync()
        {
            try
            {
                var pracownicy = new List<Pracownik>();
                using (var scope = _serviceProvider.CreateScope())
                {
                    var repository = scope.ServiceProvider.GetRequiredService<IPlacowkaRepository>();
                    pracownicy = await repository.Pracownicy.GetAllAsync();
                }
                var pracownicyList = _mapper.Map<List<PracownikDto>>(pracownicy);
                _pracownicy = new ReadOnlyCollection<PracownikDto>(pracownicyList);
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
        public ReadOnlyCollection<PracownikDto> Pracownicy => _pracownicy;
        //{
        //    get => _pracownicy;
        //}

        public PracownikDto Pracownik
        {
            get => Item.Pracownik;
            set
            {
                if(value != Item.Pracownik)
                {
                    Item.Pracownik = value;
                    OnPropertyChanged();
                    PracownikIsVisible = "Visible";
                    ClearValidationMessages();
                    if (Item.Pracownik.DniUrlopu < 1)
                        AddValidationMessage(nameof(Pracownik),
                            "Wybrany pracownik ma niewystarczająco dni urlopu");

                    OnPropertyChanged(() => DataRozpoczeciaPracy);
                    OnPropertyChanged(() => DataZawarciaUmowy);
                    OnPropertyChanged(() => OkresPracy);
                    OnPropertyChanged(() => Stanowisko);
                }
            }
        }

        private PracownikDto _WybranyZastepujacyPracownik;
        public PracownikDto WybranyZastepujacyPracownik
        {
            get => _WybranyZastepujacyPracownik;
            set
            {
                if (value != _WybranyZastepujacyPracownik)
                {
                    _WybranyZastepujacyPracownik = value;
                    base.ClearValidationMessages();
                    if (_WybranyZastepujacyPracownik?.Id == Item.Pracownik?.Id)
                        AddValidationMessage(nameof(ZastepujacyPracownik),
                            "Pracownik nie może zastępować samego siebie.");

                    OnPropertyChanged();
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

        public DateTime PoczatekUrlopu
        {
            get => Item.PoczatekUrlopu;
            set
            {
                if (value != Item.PoczatekUrlopu)
                {
                    Item.PoczatekUrlopu = value;
                    ClearErrors(nameof(PoczatekUrlopu));
                    if (Item.PoczatekUrlopu < DateTime.Today)
                        AddError(nameof(PoczatekUrlopu),
                            "Nieprawidłowa data rozpoczęcia urlopu");

                    OnPropertyChanged(() => PoczatekUrlopu);
                }
            }
        }
        public DateTime KoniecUrlopu
        {
            get => Item.KoniecUrlopu;
            set
            {
                if (value != Item.KoniecUrlopu)
                {
                    Item.KoniecUrlopu = value;
                    ClearErrors(nameof(KoniecUrlopu));
                    if (Item.KoniecUrlopu.Date < Item.PoczatekUrlopu.Date)
                        AddError(nameof(KoniecUrlopu),
                            "Nieprawidłowa data zakończenia urlopu");

                    OnPropertyChanged(() => PoczatekUrlopu);
                }
            }
        }
        public DateTime? DataRozpoczeciaPracy
        {
            get => Item.Pracownik.DataRozpoczeciaPracy;
            set
            {
                if (value != Item.Pracownik.DataRozpoczeciaPracy)
                {
                    Item.Pracownik.DataRozpoczeciaPracy = value;
                    OnPropertyChanged();
                }
            }
        }
        public DateTime? DataZawarciaUmowy
        {
            get => Item.Pracownik.DataZawarciaUmowy;
            set
            {
                if (value != Item.Pracownik.DataZawarciaUmowy)
                {
                    Item.Pracownik.DataZawarciaUmowy = value;
                    OnPropertyChanged();
                }
            }
        }
        public string PrzyczynaUrlopu 
        {
            get => Item.PrzyczynaUrlopu;
            set
            {
                if(value != Item.PrzyczynaUrlopu)
                {
                    Item.PrzyczynaUrlopu = value;
                    ClearErrors(nameof(PrzyczynaUrlopu));
                    OnPropertyChanged();
                }
            }
        }
        public string ZastepujacyPracownik
        {
            get => Item.ZastepujacyPracownik;
            set
            {
                if (value != Item.ZastepujacyPracownik)
                {
                    Item.ZastepujacyPracownik = value;
                    ClearErrors(nameof(ZastepujacyPracownik));
                    if (Item.ZastepujacyPracownik.Length < 5)
                        AddError(nameof(ZastepujacyPracownik),
                            "Należy podać imię i nazwisko pracownika zastępującego");

                    OnPropertyChanged();
                }
            }
        }
        public string OkresPracy
        {
            get => Item.Pracownik.OkresPracy;
            set
            {
                if (value != Item.Pracownik.OkresPracy)
                {
                    Item.Pracownik.OkresPracy = value;
                    OnPropertyChanged(() => OkresPracy);
                }
            }
        }
        public Stanowisko Stanowisko
        {
            get => Item.Pracownik.Stanowisko;
            set
            {
                if (value != Item.Pracownik.Stanowisko)
                {
                    Item.Pracownik.Stanowisko = value;
                    OnPropertyChanged();
                }
            }
        }
        #endregion

        #region Metody
        protected override async Task<bool> SaveAsync()
        {
            CheckRequiredProperties();
            if (ValidationMessages.Count > 0)
                IsValidationVisible = true;
            if (HasErrors || IsValidationVisible) 
                return false;

            try
            {
                //Wpis do tabeli urlopów
                MessageBox.Show("Poprawnie dodano urlop pracownikowa", "Info",
                        MessageBoxButton.OK, MessageBoxImage.Information);

                return true;
            }
            catch(Exception e)
            {
                MessageBox.Show($"Nie udało się dodać urlopu pracownika. {e.Message}", 
                    "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
        }

        //protected override bool SaveAndCloseCanExecute() =>
        //    IsEnabled && 
        //    string.IsNullOrEmpty(Zastepca) &&
        //    !IsValidationVisible;

        protected override void ClearForm()
        {
            ZastepujacyPracownik = null;
            IsEnabled = false;
            FlagsViewModel = null;
            PracownikIsVisible = "Collapsed";
            Item = new UrlopPracownikaDto { Pracownik = new PracownikDto() };
            base.ClearValidationMessages();
            ClearAllErrors();
        }

        private void CheckRequiredProperties()
        {
            if (string.IsNullOrEmpty(Pracownik.Imie))
                AddValidationMessage(nameof(Pracownik),
                    "Należy wybrać pracownika.");

            base.CheckRequiredProperties(
                nameof(Pracownik),
                nameof(PoczatekUrlopu),
                nameof(KoniecUrlopu),
                nameof(ZastepujacyPracownik),
                nameof(PrzyczynaUrlopu)
                );
        }

        #endregion
    }
}
