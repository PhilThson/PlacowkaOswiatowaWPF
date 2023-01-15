using AutoMapper;
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
    public class NowaUmowaViewModel : SingleItemViewModel<UmowaDto>, ILoadable
    {
        #region Konstruktor
        public NowaUmowaViewModel(IPlacowkaRepository repository, IMapper mapper)
            : base(BaseResources.NowaUmowa, repository, mapper)
        {
            Item = new UmowaDto { Etat = new Etat(), Stanowisko = new Stanowisko() };
        }
        #endregion

        #region Właściwości umowy
        private ReadOnlyCollection<PracownikDto> _pracownicy;
        public ReadOnlyCollection<PracownikDto> Pracownicy
        {
            get => _pracownicy;
            set
            {
                _pracownicy = value;
                OnPropertyChanged(() => Pracownicy);
            }
        }

        //private PracownikDto _wybranyPracownik;
        public PracownikDto WybranyPracownik
        {
            get => Item.Pracownik;
            set
            {
                Item.Pracownik = value;
                OnPropertyChanged(() => WybranyPracownik);
                if (value != null)
                {
                    //RaisePracownikChaged();
                    //PracownikIsVisible = "Visible";
                }
            }
        }
        private ReadOnlyCollection<PracodawcaDto> _pracodawcy;
        public ReadOnlyCollection<PracodawcaDto> Pracodawcy
        {
            get => _pracodawcy;
            set
            {
                _pracodawcy = value;
                OnPropertyChanged(() => Pracodawcy);
            }
        }
        //private Pracodawca _wybranyPracodawca;
        public PracodawcaDto WybranyPracodawca
        {
            get => Item.Pracodawca;
            set
            {
                if (value != Item.Pracodawca)
                {
                    Item.Pracodawca = value;
                    OnPropertyChanged(() => WybranyPracodawca);
                    //RaisePracownikChaged();
                    //PracownikIsVisible = "Visible";
                }
            }
        }
        public DateTime? DataRozpoczeciaPracy
        {
            get => Item.DataRozpoczeciaPracy;
            set
            {
                if (value != Item.DataRozpoczeciaPracy)
                {
                    Item.DataRozpoczeciaPracy = value;
                    OnPropertyChanged(() => DataRozpoczeciaPracy);
                }
            }
        }
        public DateTime? DataZakonczeniaPracy
        {
            get => Item.DataZakonczeniaPracy;
            set
            {
                if (value != Item.DataZakonczeniaPracy)
                {
                    Item.DataZakonczeniaPracy = value;
                    OnPropertyChanged(() => DataZakonczeniaPracy);
                }
            }
        }
        public DateTime? DataZawarciaUmowy
        {
            get => Item.DataZawarciaUmowy;
            set
            {
                if (value != Item.DataZawarciaUmowy)
                {
                    Item.DataZawarciaUmowy = value;
                    OnPropertyChanged(() => DataZawarciaUmowy);
                }
            }
        }
        public bool CzyZwolnionyOdPodatku
        {
            get => Item.CzyZwolnionyOdPodatku;
            set
            {
                if (value != Item.CzyZwolnionyOdPodatku)
                {
                    Item.CzyZwolnionyOdPodatku = value;
                    OnPropertyChanged(() => CzyZwolnionyOdPodatku);
                }
            }
        }
        public string OpisWynagrodzenia
        {
            get => Item.OpisWynagrodzenia;
            set
            {
                if (value != Item.OpisWynagrodzenia)
                {
                    Item.OpisWynagrodzenia = value;
                    OnPropertyChanged(() => OpisWynagrodzenia);
                }
            }
        }
        public string MiejsceWykonywaniaPracy
        {
            get => Item.MiejsceWykonywaniaPracy;
            set
            {
                if (value != Item.MiejsceWykonywaniaPracy)
                {
                    Item.MiejsceWykonywaniaPracy = value;
                    OnPropertyChanged(() => MiejsceWykonywaniaPracy);
                }
            }
        }
        public string WymiarCzasuPracy
        {
            get => Item.WymiarCzasuPracy;
            set
            {
                if (value != Item.WymiarCzasuPracy)
                {
                    Item.WymiarCzasuPracy = value;
                    OnPropertyChanged(() => WymiarCzasuPracy);
                }
            }
        }
        public double? WymiarGodzinowy
        {
            get => Item.WymiarGodzinowy;
            set
            {
                if (value != Item.WymiarGodzinowy)
                {
                    Item.WymiarGodzinowy = value;
                    OnPropertyChanged(() => WymiarGodzinowy);
                }
            }
        }
        public string OkresPracy
        {
            get => Item.OkresPracy;
            set
            {
                if (value != Item.OkresPracy)
                {
                    Item.OkresPracy = value;
                    OnPropertyChanged(() => OkresPracy);
                }
            }
        }
        public string InneWarunkiZatrudnienia
        {
            get => Item.InneWarunkiZatrudnienia;
            set
            {
                if (value != Item.InneWarunkiZatrudnienia)
                {
                    Item.InneWarunkiZatrudnienia = value;
                    OnPropertyChanged(() => InneWarunkiZatrudnienia);
                }
            }
        }
        public string PrzyczynyZawarciaUmowy
        {
            get => Item.PrzyczynyZawarciaUmowy;
            set
            {
                if (value != Item.PrzyczynyZawarciaUmowy)
                {
                    Item.PrzyczynyZawarciaUmowy = value;
                    OnPropertyChanged(() => PrzyczynyZawarciaUmowy);
                }
            }
        }
        private ReadOnlyCollection<Stanowisko> _stanowiska;
        public ReadOnlyCollection<Stanowisko> Stanowiska
        {
            get => _stanowiska;
            set
            {
                _stanowiska = value;
                OnPropertyChanged(() => Stanowiska);
            }
        }
        public Stanowisko WybraneStanowisko
        {
            get => Item.Stanowisko;
            set
            {
                if (value != Item.Stanowisko)
                {
                    Item.Stanowisko = value;
                    OnPropertyChanged(() => WybraneStanowisko);
                }
            }
        }
        private ReadOnlyCollection<Etat> _etaty;
        public ReadOnlyCollection<Etat> Etaty
        {
            get => _etaty;
            set
            {
                _etaty = value;
                OnPropertyChanged(() => Etaty);
            }
        }
        public Etat WybranyEtat
        {
            get => Item.Etat;
            set
            {
                if (value != Item.Etat)
                {
                    Item.Etat = value;
                    OnPropertyChanged(() => WybranyEtat);
                }
            }
        }
        public decimal WynagrodzenieBrutto
        {
            get => Item.WynagrodzenieBrutto;
            set
            {
                if (value != Item.WynagrodzenieBrutto)
                {
                    Item.WynagrodzenieBrutto = value;
                    OnPropertyChanged(() => WynagrodzenieBrutto);
                }
            }
        }
        #endregion

        #region Pobranie zasobów z bazy danych
        public async Task LoadAsync()
        {
            try
            {
                var etatyFromDb = await _repository.Etaty.GetAllAsync();
                Etaty = new ReadOnlyCollection<Etat>(etatyFromDb);

                var stanowiskaFromDb = await _repository.Stanowiska.GetAllAsync();
                Stanowiska = new ReadOnlyCollection<Stanowisko>(stanowiskaFromDb);

                var pracownicyFormDb = await _repository.Pracownicy.GetAllAsync();
                var listaPracownikow = _mapper.Map<List<PracownikDto>>(pracownicyFormDb);
                Pracownicy = new ReadOnlyCollection<PracownikDto>(listaPracownikow);

                var pracodawcyFromDb = await _repository.Pracodawcy.GetAllAsync();
                var listaPracodawcow = _mapper.Map<List<PracodawcaDto>>(pracodawcyFromDb);
                Pracodawcy = new ReadOnlyCollection<PracodawcaDto>(listaPracodawcow);
            }
            catch (Exception)
            {
                MessageBox.Show("Nie udało się załadować danych.", "Error",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        #endregion

        #region Obsługa komend
        protected override async Task SaveAsync()
        {
            try
            {
                var nowaUmowa = Item;
                var umowa = _mapper.Map<Umowa>(nowaUmowa);
                var czyIstnieje = await _repository.Umowy.Exists(Item);
                if (!czyIstnieje)
                {
                    await _repository.Umowy.AddAsync(umowa);
                    await _repository.SaveAsync();

                    MessageBox.Show("Dodano umowę!", "Sukces",
                    MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show("Umowa pomiędzy wybranymi podmiotami już istnieje.", 
                        "Uwaga",
                    MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Nie udało się dodać umowy", "Error",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        #endregion
    }
}
