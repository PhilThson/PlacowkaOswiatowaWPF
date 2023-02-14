using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using PlacowkaOswiatowa.Domain.BusinessLogic;
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
    public class ZarobkiPracownikaViewModel : SingleItemViewModel<Skladki>, ILoadable
    {
        #region Pola prywatne
        private readonly ILogger<ZarobkiPracownikaViewModel> _logger;
        #endregion

        #region Konstruktor
        public ZarobkiPracownikaViewModel(IServiceProvider serviceProvider, IMapper mapper,
            ILogger<ZarobkiPracownikaViewModel> logger)
            : base(serviceProvider, mapper, BaseResources.ZarobkiPracownika)
        {
            _pracownikIsVisible = "Collapsed";
            Item = new Skladki()
            {
                Pracownik = new PracownikDto()
            };
            _logger = logger;
        }
        #endregion

        #region Pola, właściwości

        private ReadOnlyCollection<PracownikDto> _pracownicy;
        public ReadOnlyCollection<PracownikDto> Pracownicy
        {
            get => _pracownicy;
        }

        public PracownikDto Pracownik
        {
            get
            {
                //W celu wyzerowania ComboBoxa
                if (string.IsNullOrEmpty(Item.Pracownik?.Nazwisko))
                    return null;
                return Item.Pracownik;
            }
            set
            {
                if (value != Item.Pracownik)
                {
                    Item.Pracownik = value;
                    PracownikIsVisible = "Visible";
                    ClearValidationMessages();
                    ClearAllErrors();
                    foreach (var prop in Pracownik.GetType().GetProperties())
                        OnPropertyChanged(prop.Name);

                    OnPropertyChanged(() => Pracownik);
                }
            }
        }

        private string _pracownikIsVisible;
        public string PracownikIsVisible
        {
            get => _pracownikIsVisible;
            set
            {
                if (value != _pracownikIsVisible)
                {
                    _pracownikIsVisible = value;
                    OnPropertyChanged(() => PracownikIsVisible);
                }
            }
        }

        #endregion

        #region Dane pracownika

        public decimal WynagrodzenieBrutto
        {
            get => Item.Pracownik.WynagrodzenieBrutto;
            set
            {
                if (value != Item.Pracownik.WynagrodzenieBrutto)
                {
                    Item.Pracownik.WynagrodzenieBrutto = value;
                    ClearErrors(nameof(WynagrodzenieBrutto));
                    if (Item.Pracownik.WynagrodzenieBrutto == default)
                        AddError(nameof(WynagrodzenieBrutto),
                            "Wynagrodzenie brutto jest wymagane");

                    OnPropertyChanged(() => WynagrodzenieBrutto);
                }
            }
        }
        public double? WymiarGodzinowy
        {
            get => Item.Pracownik.WymiarGodzinowy;
            set
            {
                if (value != Item.Pracownik.WymiarGodzinowy)
                {
                    Item.Pracownik.WymiarGodzinowy = value;
                    ClearErrors(nameof(WymiarGodzinowy));
                    if (Item.Pracownik.WymiarGodzinowy == null ||
                        Item.Pracownik.WymiarGodzinowy == default(double))
                        AddError(nameof(WymiarGodzinowy),
                            "Wymiar godzinowy jest wymagany");

                    OnPropertyChanged(() => WymiarGodzinowy);
                }
            }
        }
        public double? Nadgodziny
        {
            get => Item.Pracownik.Nadgodziny;
            set
            {
                if (value != Item.Pracownik.Nadgodziny)
                {
                    Item.Pracownik.Nadgodziny = value;
                    OnPropertyChanged(() => Nadgodziny);
                }
            }
        }

        public decimal SkladkaEmerytalnaProcent
        {
            get => Item.SkladkaEmerytalnaProcent;
            set
            {
                if (value != Item.SkladkaEmerytalnaProcent)
                {
                    Item.SkladkaEmerytalnaProcent = value;
                    ClearErrors(nameof(SkladkaEmerytalnaProcent));
                    if (Item.SkladkaEmerytalnaProcent < 0 ||
                        Item.SkladkaEmerytalnaProcent > 100)
                        AddErrorForPercentage(nameof(SkladkaEmerytalnaProcent));

                    OnPropertyChanged();
                }
            }
        }
        public decimal SkladkaRentowaProcent
        {
            get => Item.SkladkaRentowaProcent;
            set
            {
                if (value != Item.SkladkaRentowaProcent)
                {
                    Item.SkladkaRentowaProcent = value;
                    ClearErrors(nameof(SkladkaRentowaProcent));
                    if (Item.SkladkaRentowaProcent < 0 ||
                        Item.SkladkaRentowaProcent > 100)
                        AddErrorForPercentage(nameof(SkladkaRentowaProcent));

                    OnPropertyChanged();
                }
            }
        }
        public decimal SkladkaChorobowaProcent
        {
            get => Item.SkladkaChorobowaProcent;
            set
            {
                if (value != Item.SkladkaChorobowaProcent)
                {
                    Item.SkladkaChorobowaProcent = value;
                    ClearErrors(nameof(SkladkaChorobowaProcent));
                    if (Item.SkladkaChorobowaProcent < 0 ||
                        Item.SkladkaChorobowaProcent > 100)
                        AddErrorForPercentage(nameof(SkladkaChorobowaProcent));

                    OnPropertyChanged();
                }
            }
        }
        public decimal SkladkaZdrowotnaProcent
        {
            get => Item.SkladkaZdrowotnaProcent;
            set
            {
                if (value != Item.SkladkaZdrowotnaProcent)
                {
                    Item.SkladkaZdrowotnaProcent = value;
                    ClearErrors(nameof(SkladkaZdrowotnaProcent));
                    if (Item.SkladkaZdrowotnaProcent < 0 ||
                        Item.SkladkaZdrowotnaProcent > 100)
                        AddErrorForPercentage(nameof(SkladkaZdrowotnaProcent));

                    OnPropertyChanged();
                }
            }
        }
        public decimal KosztUzyskaniaPrzychodu
        {
            get => Item.KosztUzyskaniaPrzychodu;
            set
            {
                if (value != Item.KosztUzyskaniaPrzychodu)
                {
                    Item.KosztUzyskaniaPrzychodu = value;
                    OnPropertyChanged();
                }
            }
        }
        public decimal PodatekProcent
        {
            get => Item.PodatekProcent;
            set
            {
                if (value != Item.PodatekProcent)
                {
                    Item.PodatekProcent = value;
                    ClearErrors(nameof(PodatekProcent));
                    if (Item.PodatekProcent < 0 ||
                        Item.PodatekProcent > 100)
                        AddErrorForPercentage(nameof(PodatekProcent));

                    OnPropertyChanged();
                }
            }
        }
        public decimal KwotaWolnaOdPodatku
        {
            get => Item.KwotaWolnaOdPodatku;
            set
            {
                if (value != Item.KwotaWolnaOdPodatku)
                {
                    Item.KwotaWolnaOdPodatku = value;
                    ClearErrors(nameof(KwotaWolnaOdPodatku));
                    if (Item.KwotaWolnaOdPodatku < 0 ||
                        Item.KwotaWolnaOdPodatku > 500)
                        AddError(nameof(KwotaWolnaOdPodatku),
                            "Kwota musi być z zakresu od 0 do 500 zł");

                    OnPropertyChanged();
                }
            }
        }
        #endregion

        #region Właściwości do odczytu
        public decimal SkladkaEmerytalna
        {
            get => Item.SkladkaEmerytalna;
        }
        public decimal SkladkaRentowa
        {
            get => Item.SkladkaRentowa;
        }
        public decimal SkladkaChorobowa
        {
            get => Item.SkladkaChorobowa;
        }
        public decimal SkladkaZdrowotna
        {
            get => Item.SkladkaZdrowotna;
        }
        public decimal ZaliczkaNaPodatekDochodowy
        {
            get => Item.ZaliczkaNaPodatekDochodowy;
        }
        #endregion

        #region Wartości wyliczone

        public decimal WynagrodzenieNetto
        {
            get => Item.WynagrodzenieNetto;
        }

        public decimal StawkaNaGodzine
        {
            get => Item.StawkaNaGodzine;
        }

        #endregion

        #region Inicjacja

        public async Task LoadAsync()
        {
            var pracownicy = new List<Pracownik>();
            using (var scope = _serviceProvider.CreateScope())
            {
                var repository = scope.ServiceProvider.GetRequiredService<IPlacowkaRepository>();
                pracownicy = await repository.Pracownicy.GetAllAsync();
            }
            var listaPracownikow = _mapper.Map<List<PracownikDto>>(pracownicy);
            _pracownicy = new ReadOnlyCollection<PracownikDto>(listaPracownikow);
            OnPropertyChanged(() => Pracownicy);
        }
        #endregion

        #region Metody

        protected override async Task<bool> SaveAsync()
        {
            try
            {
                Item.ObliczNetto();
                Item.ObliczStawkeNaGodzine();

                var skladkiDto = _mapper.Map<SkladkiDto>(Item);

                using (var scope = _serviceProvider.CreateScope())
                {
                    var repository = scope.ServiceProvider.GetRequiredService<IPlacowkaRepository>();
                    var pracownik = await repository.Pracownicy.GetByIdAsync((int)Item.Pracownik.Id, true);
                    pracownik.ObliczoneWynagrodzenieNetto = Item.WynagrodzenieNetto;
                    pracownik.ObliczonaStawkaNaGodzineNetto = Item.StawkaNaGodzine;
                    pracownik.DataOstatnichObliczen = DateTime.Now;
                    pracownik.ParametryObliczen = JsonConvert.SerializeObject(skladkiDto);

                    await repository.SaveAsync();
                }

                foreach (var prop in this.GetType().GetProperties())
                    OnPropertyChanged(prop.Name);

                MessageBox.Show($"Obliczono wynagrodzenie pracownika " +
                    $"{Item.Pracownik.Imie} {Item.Pracownik.Nazwisko}", "Info",
                    MessageBoxButton.OK, MessageBoxImage.Information);

                _logger.LogInformation(
                    "Obliczono wynagrodzenie pracownika: {imie} {nazwisko}, pensja: {pensja}",
                    Item.Pracownik.Imie, Item.Pracownik.Nazwisko, Item.WynagrodzenieNetto);
            }
            catch (Exception e)
            {
                MessageBox.Show($"Nie udało się obliczyć wynagrodzenia pracownika. {e.Message}",
                    "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);

                _logger.LogError(
                    "Błąd podczas obliczania wynagrodzenia pracownika: {error}",
                    e.Message);
            }
            //Ten formularz nie zamyka się automatycznie
            return await Task.FromResult(false);
        }

        protected override void CheckRequiredProperties()
        {
            if (string.IsNullOrEmpty(Item.Pracownik.Nazwisko))
                AddValidationMessage(nameof(Pracownik), "Należy wybrać pracownika.");

            BaseCheckRequiredProperties(
                nameof(Pracownik),
                nameof(SkladkaEmerytalnaProcent),
                nameof(SkladkaRentowaProcent),
                nameof(SkladkaChorobowaProcent),
                nameof(SkladkaZdrowotnaProcent),
                nameof(PodatekProcent),
                "Pracownik.WymiarGodzinowy",
                "Pracownik.WynagrodzenieBrutto"
                );
        }

        protected override void ClearForm(object _)
        {
            Item = new Skladki()
            {
                Pracownik = new PracownikDto()
            };
            ClearAllErrors();
            ClearValidationMessages();
            foreach (var prop in this.GetType().GetProperties())
                OnPropertyChanged(prop.Name);
        }

        #endregion
    }
}
