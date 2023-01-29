using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using PlacowkaOswiatowa.Domain.DTOs;
using PlacowkaOswiatowa.Domain.Exceptions;
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
    public class NowaUmowaViewModel : SingleItemViewModel<UmowaDto>, 
        ILoadable, IEditable
    {
        #region Konstruktor
        public NowaUmowaViewModel(IServiceProvider serviceProvider, IMapper mapper)
            : base(serviceProvider, mapper, BaseResources.NowaUmowa)
        {
            Item = new UmowaDto();
        }
        #endregion

        #region Właściwości umowy
        private bool _IsForEdit;
        public bool IsForEdit 
        {
            get => _IsForEdit;
            set => SetProperty(ref _IsForEdit, value);
        }

        private ObservableCollection<PracownikDto> _pracownicy;
        public ObservableCollection<PracownikDto> Pracownicy
        {
            get => _pracownicy;
            set
            {
                _pracownicy = value;
                OnPropertyChanged(() => Pracownicy);
            }
        }

        public PracownikDto Pracownik
        {
            get => Item.Pracownik;
            set
            {
                if (value != Item.Pracownik && value != null) 
                {
                    Item.Pracownik = value;
                    ClearErrors(nameof(Pracownik));

                    OnPropertyChanged(() => Pracownik);
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

        public PracodawcaDto Pracodawca
        {
            get => Item.Pracodawca;
            set
            {
                if (value != Item.Pracodawca)
                {
                    Item.Pracodawca = value;
                    ClearErrors(nameof(Pracodawca));
                    OnPropertyChanged(() => Pracodawca);
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
                    ClearErrors(nameof(DataRozpoczeciaPracy));
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
                    ClearErrors(nameof(DataZawarciaUmowy));
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
                    ClearErrors(nameof(MiejsceWykonywaniaPracy));
                    if (Item.MiejsceWykonywaniaPracy.Length < 1)
                        AddError(nameof(MiejsceWykonywaniaPracy), 
                            "Należy podać miejsce wykonywanej pracy");

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
                    ClearErrors(nameof(WymiarCzasuPracy));
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
                    ClearErrors(nameof(OkresPracy));
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
        private ObservableCollection<Stanowisko> _stanowiska;
        public ObservableCollection<Stanowisko> Stanowiska
        {
            get => _stanowiska;
            set
            {
                _stanowiska = value;
                OnPropertyChanged(() => Stanowiska);
            }
        }
        public Stanowisko Stanowisko
        {
            get => Item.Stanowisko;
            set
            {
                if (value != Item.Stanowisko)
                {
                    Item.Stanowisko = value;
                    ClearErrors(nameof(Stanowisko));
                    OnPropertyChanged(() => Stanowisko);
                }
            }
        }
        private ObservableCollection<Etat> _etaty;
        public ObservableCollection<Etat> Etaty
        {
            get => _etaty;
            set
            {
                _etaty = value;
                OnPropertyChanged(() => Etaty);
            }
        }
        public Etat Etat
        {
            get => Item.Etat;
            set
            {
                if (value != Item.Etat)
                {
                    Item.Etat = value;
                    ClearErrors(nameof(Etat));
                    OnPropertyChanged(() => Etat);
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
                    ClearErrors(nameof(WynagrodzenieBrutto));
                    if (Item.WynagrodzenieBrutto == 0)
                        AddError(nameof(WynagrodzenieBrutto), "Należy podać wynagrodzenie");

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
                var pracownicy = new List<Pracownik>();
                var pracodawcy = new List<Pracodawca>();
                var etaty = new List<Etat>();
                var stanowiska = new List<Stanowisko>();
                using (var scoper = _serviceProvider.CreateScope())
                {
                    var repository = scoper.ServiceProvider.GetRequiredService<IPlacowkaRepository>();

                    pracownicy = await repository.Pracownicy.GetAllAsync(
                        p => p.PracownikUmowa == null, "PracownikUmowa");
                    pracodawcy = await repository.Pracodawcy.GetAllAsync();
                    etaty = await repository.Etaty.GetAllAsync();
                    stanowiska = await repository.Stanowiska.GetAllAsync();
                }
                var listaPracownikow = _mapper.Map<List<PracownikDto>>(pracownicy);
                Pracownicy = new ObservableCollection<PracownikDto>(listaPracownikow);

                var listaPracodawcow = _mapper.Map<List<PracodawcaDto>>(pracodawcy);
                Pracodawcy = new ReadOnlyCollection<PracodawcaDto>(listaPracodawcow);

                Etaty = new ObservableCollection<Etat>(etaty);
                Stanowiska = new ObservableCollection<Stanowisko>(stanowiska);
            }
            catch (Exception)
            {
                MessageBox.Show("Nie udało się załadować danych.", "Error",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public async Task LoadItem(object objId)
        {
            try
            {
                Umowa umowa = null;
                var umowaId = Convert.ToInt32(objId);
                if (umowaId == default)
                    throw new ArgumentException("Przesłano nieprawidłowy identyfikator obiektu");

                using (var scope = _serviceProvider.CreateScope())
                {
                    var repository = scope.ServiceProvider.GetRequiredService<IPlacowkaRepository>();
                    umowa = await repository.Umowy.GetByIdAsync(umowaId) ?? 
                        throw new DataNotFoundException(
                            $"Nie znaleziono umowy o podanym identyfikatorze ({umowaId})");
                }

                Item = _mapper.Map<UmowaDto>(umowa);

                base.DisplayName = BaseResources.EdycjaUmowy;
                base.AddItemName = BaseResources.SaveItem;
                IsForEdit = true;

                foreach (var prop in Item.GetType().GetProperties())
                    this.OnPropertyChanged(prop.Name);
            }
            catch(Exception e)
            {
                MessageBox.Show($"Nie udało się zainicjalizować obiektu. {e.Message}",
                    "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        #endregion

        #region Obsługa komend
        protected override async Task<bool> SaveAsync()
        {
            try
            {
                using (var scope = _serviceProvider.CreateScope())
                {
                    var repository = scope.ServiceProvider.GetRequiredService<IPlacowkaRepository>();
                    var umowa = _mapper.Map<Umowa>(Item);
                    if (umowa.Id == default)
                    {
                        //nie jest spradzane, ponieważ do wyboru są tylko pracownicy bez umowy
                        //var czyIstnieje = await _repository.Umowy.Exists(Item);
                        //if (czyIstnieje)
                        //    throw new DataValidationException(
                        //        "Umowa pomiędzy wybranymi podmiotami już istnieje.");

                        await repository.AddAsync(umowa);
                    }
                    else
                        repository.Update(umowa);

                    await repository.SaveAsync();
                }

                MessageBox.Show("Zapisano umowę!", "Sukces",
                    MessageBoxButton.OK, MessageBoxImage.Information);

                return true;
            }
            catch(DataValidationException e)
            {
                MessageBox.Show(e.Message, "Uwaga",
                    MessageBoxButton.OK, MessageBoxImage.Warning);

                return false;
            }
            catch (Exception)
            {
                MessageBox.Show("Nie udało się dodać umowy", "Error",
                    MessageBoxButton.OK, MessageBoxImage.Error);

                return false;
            }
        }

        //protected override void ClearForm()
        //{
        //    Item = new UmowaDto();
        //    ClearAllErrors();
        //}
        #endregion

        #region Metody prywatne
        protected override void CheckRequiredProperties() =>
            BaseCheckRequiredProperties(
                nameof(Pracownik),
                nameof(Pracodawca),
                nameof(WynagrodzenieBrutto),
                nameof(MiejsceWykonywaniaPracy),
                nameof(WymiarCzasuPracy),
                nameof(OkresPracy),
                nameof(DataZawarciaUmowy),
                nameof(Etat),
                nameof(Stanowisko),
                nameof(DataRozpoczeciaPracy)
                );

        #endregion
    }
}
