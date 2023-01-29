using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
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

        #region Konstruktor
        public ZarobkiPracownikaViewModel(IServiceProvider serviceProvider, IMapper mapper)
            : base(serviceProvider, mapper, BaseResources.ZarobkiPracownika)
        {
            _pracownikIsVisible = "Collapsed";
            Item = new Skladki()
            {
                Pracownik = new PracownikDto()
            };
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
                if(value != _pracownikIsVisible)
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
                        AddErrorForRequired(nameof(WynagrodzenieBrutto));

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
                        AddErrorForRequired(nameof(WymiarGodzinowy));

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
                if(value != Item.SkladkaEmerytalnaProcent)
                {
                    Item.SkladkaEmerytalnaProcent = value;
                    ClearErrors(nameof(SkladkaEmerytalnaProcent));
                    if (Item.SkladkaEmerytalnaProcent == default)
                        AddErrorForRequired(nameof(SkladkaEmerytalnaProcent));

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
                    if (Item.SkladkaRentowaProcent == default)
                        AddErrorForRequired(nameof(SkladkaRentowaProcent));

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
                    if (Item.SkladkaChorobowaProcent == default)
                        AddErrorForRequired(nameof(SkladkaChorobowaProcent));

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
                    if (Item.SkladkaZdrowotna == default)
                        AddErrorForRequired(nameof(SkladkaZdrowotna));

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
                    if (Item.PodatekProcent == default)
                        AddErrorForRequired(nameof(PodatekProcent));

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
                    OnPropertyChanged();
                }
            }
        }

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


        #endregion

        #region Inicjacja

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
                var listaPracownikow = _mapper.Map<List<PracownikDto>>(pracownicy);
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
            }
            catch(Exception e)
            {
                MessageBox.Show($"Nie udało się obliczyć wynagrodzenia pracownika. {e.Message}",
                    "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
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
