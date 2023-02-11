using PlacowkaOswiatowa.Domain.Interfaces.RepositoryInterfaces;
using PlacowkaOswiatowa.Domain.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;
using AutoMapper;
using PlacowkaOswiatowa.Domain.Interfaces.CommonInterfaces;
using PlacowkaOswiatowa.Domain.Resources;
using PlacowkaOswiatowa.ViewModels.Abstract;
using PlacowkaOswiatowa.Domain.DTOs;
using PlacowkaOswiatowa.Domain.Exceptions;
using Microsoft.Extensions.DependencyInjection;
using PlacowkaOswiatowa.Domain.Helpers;
using System.Windows.Input;
using PlacowkaOswiatowa.Domain.Commands;

namespace PlacowkaOswiatowa.ViewModels
{
    public class NowyUczenViewModel : SingleItemViewModel<UczenDto>, 
        ILoadable, IEditable
    {
        #region Pola prywatne
        private readonly ISignalHub _signal;
        #endregion

        #region Konstruktor
        public NowyUczenViewModel(IServiceProvider serviceProvider, IMapper mapper)
            : base(serviceProvider, mapper, BaseResources.NowyUczen)
        {
            Item = new UczenDto();
            _signal = SignalHub.Instance;
            AddressButtonContent = BaseResources.DodajAdres;
        }

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

                    base.ClearErrors(nameof(Imie));
                    if (Item.Imie.Length < 3)
                        base.AddError(nameof(Imie), "Imię musi posiadać przynajmniej 3 znaki");

                    OnPropertyChanged(() => Imie);
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

                    base.ClearErrors(nameof(DrugieImie));
                    if (Item.DrugieImie.Length != 0 && Item.DrugieImie.Length < 3)
                        base.AddError(nameof(DrugieImie), "Drugie imię musi posiadać przynajmniej 3 znaki");

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

                    base.ClearErrors(nameof(Nazwisko));
                    if (Item.Nazwisko.Length < 3)
                        base.AddError(nameof(Nazwisko), "Nazwisko musi posiadać przynajmniej 3 znaki");

                    OnPropertyChanged(() => Nazwisko);
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
                    base.ClearErrors(nameof(Pesel));
                    if (Item.Pesel.Length < 11)
                        base.AddError(nameof(Pesel), "Pesel musi posiadać 11 znaków");
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
                    base.ClearErrors(nameof(DataUrodzenia));
                    if (Item.DataUrodzenia > DateTime.Today)
                        base.AddError(nameof(DataUrodzenia),
                            "Nieprawidłowa data urodzenia");

                    OnPropertyChanged(() => DataUrodzenia);
                }
            }
        }
        #endregion

        #region Pola i właściwości Ucznia

        private ObservableCollection<OddzialDto> _oddzialy;
        public ObservableCollection<OddzialDto> Oddzialy
        {
            get => _oddzialy;
            set => SetProperty(ref _oddzialy, value);
        }

        public OddzialDto Oddzial
        {
            get => Item.Oddzial;
            set
            {
                if(value != Item.Oddzial)
                {
                    Item.Oddzial = value;
                    ClearErrors(nameof(Oddzial));
                    OnPropertyChanged();
                }
            }
        }

        public AdresDto Adres
        {
            get => Item.Adres;
        }

        private string _AddressButtonContent;
        public string AddressButtonContent 
        {
            get => _AddressButtonContent;
            set => SetProperty(ref _AddressButtonContent, value);
        }

        #endregion

        #region Pola i własności Adresu
        //public string Panstwo
        //{
        //    get => Item.Adres.Panstwo;
        //    set 
        //    { 
        //        if(value != Item.Adres.Panstwo)
        //        {
        //            Item.Adres.Panstwo = value;

        //            ClearErrors(nameof(Panstwo));
        //            if (Item.Adres.Panstwo.Length < 1)
        //                AddError(nameof(Panstwo), "Należy podać Państwo");

        //            OnPropertyChanged(() => Panstwo);
        //        }
        //    }
        //}
        //public string Miejscowosc
        //{
        //    get => Item.Adres.Miejscowosc;
        //    set 
        //    { 
        //        if(value != Item.Adres.Miejscowosc)
        //        {
        //            Item.Adres.Miejscowosc = value;

        //            ClearErrors(nameof(Miejscowosc));
        //            if (Item.Adres.Miejscowosc.Length < 1)
        //                AddError(nameof(Miejscowosc), "Należy podać miejscowość");

        //            OnPropertyChanged(() => Miejscowosc);
        //        }
        //    }
        //}
        //public string Ulica
        //{
        //    get => Item.Adres.Ulica;
        //    set
        //    {
        //        if (value != Item.Adres.Ulica)
        //        {
        //            Item.Adres.Ulica = value;
        //            OnPropertyChanged(() => Ulica);
        //        }
        //    }
        //}
        //public string NumerDomu
        //{
        //    get => Item.Adres.NumerDomu;
        //    set
        //    {
        //        if (value != Item.Adres.NumerDomu)
        //        {
        //            Item.Adres.NumerDomu = value;
        //            ClearErrors(nameof(NumerDomu));
        //            if (Item.Adres.NumerDomu.Length < 1)
        //                AddError(nameof(NumerDomu), "Należy podać numer domu");
        //            OnPropertyChanged(() => NumerDomu);
        //        }
        //    }
        //}
        //public string NumerMieszkania
        //{
        //    get => Item.Adres.NumerMieszkania;
        //    set
        //    {
        //        if (value != Item.Adres.NumerMieszkania)
        //        {
        //            Item.Adres.NumerMieszkania = value;
        //            OnPropertyChanged(() => NumerMieszkania);
        //        }
        //    }
        //}
        //public string KodPocztowy
        //{
        //    get => Item.Adres.KodPocztowy;
        //    set
        //    {
        //        if (value != Item.Adres.KodPocztowy)
        //        {
        //            Item.Adres.KodPocztowy = value;
        //            ClearErrors(nameof(KodPocztowy));
        //            if (Item.Adres.KodPocztowy.Length < 1)
        //                AddError(nameof(KodPocztowy), "Należy podać kod pocztowy");
        //            OnPropertyChanged(() => KodPocztowy);
        //        }
        //    }
        //}
        #endregion

        #region Komendy

        private ICommand _AddAddressCommand;
        public ICommand AddAddressCommand => _AddAddressCommand ??=
            new BaseCommand(AddAddress);

        private void AddAddress()
        {
            var viewHandler = new ViewHandler(typeof(NowyAdresViewModel), 
                Item.Adres?.Id, isModal: true);

            var listenerId = Guid.NewGuid();
            _signal.AddAddressCreatedListener(listenerId, this.OnAddressCreated);
            _signal.RaiseCreateView(listenerId, viewHandler);
        }

        #endregion

        #region Metody

        private void OnAddressCreated(AdresDto createdAddress, Guid listenerId)
        {
            Item.Adres = createdAddress;
            OnPropertyChanged(nameof(Adres));
            AddressButtonContent = BaseResources.EdycjaAdresu;
        }

        protected override async Task<bool> SaveAsync()
        {
            try
            {
                var uczen = _mapper.Map<Uczen>(Item);
                if (uczen.OddzialId != default)
                    uczen.Oddzial = null;

                using (var scope = _serviceProvider.CreateScope())
                {
                    var repository = scope.ServiceProvider.GetRequiredService<IPlacowkaRepository>();

                    if(uczen.Id != default)
                    {
                        var uczenById = await repository.Uczniowie.GetByIdAsync(uczen.Id);
                        if (uczenById == uczen)
                            throw new DataValidationException("Nie dokonano zmian");
                    }

                    var uczenFromDb = await repository.Uczniowie.GetUczenByPesel(uczen.Pesel);

                    if (uczenFromDb is not null)
                        if(uczenFromDb.Id != uczen.Id)
                            throw new DataValidationException("Uczeń o podanym nr PESEL już istnieje");
                    
                    if (Item.Adres?.Id != null)
                    {
                        var adresId = Convert.ToInt32(Item.Adres?.Id);
                        if (adresId != default)
                            uczen.AdresId = adresId;
                    }

                    if (uczen.Id == default)
                        await repository.AddAsync(uczen);
                    else
                        repository.Update(uczen);

                    await repository.SaveAsync();
                }

                MessageBox.Show("Zapisano ucznia!", "Sukces",
                    MessageBoxButton.OK, MessageBoxImage.Information);

                _signal.RaiseRequestRefreshStudentsView();

                return true;
            }
            catch(DataValidationException e)
            {
                MessageBox.Show(e.Message, "Uwaga", 
                    MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            catch (Exception e)
            {
                MessageBox.Show($"Nie udało się zapisać ucznia. {e.Message}", 
                    "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            return false;
        }

        protected override void CheckRequiredProperties() =>
            BaseCheckRequiredProperties(
                nameof(Imie),
                nameof(Nazwisko),
                nameof(DataUrodzenia),
                nameof(Pesel),
                nameof(Oddzial));

        #endregion

        #region Inicjacja

        public async Task LoadAsync()
        {
            try
            {
                var oddzialyFromDb = new List<Oddzial>();
                //Zabezpieczenie przed próbą dostępu do db kontekstu, który jest w użyciu
                using (var scope = _serviceProvider.CreateScope())
                {
                    var repository = scope.ServiceProvider.GetRequiredService<IPlacowkaRepository>();
                    oddzialyFromDb = await repository.Oddzialy.GetAllAsync(includeProperties: "Pracownik");
                }
                
                var listaOddzialow = _mapper.Map<List<OddzialDto>>(oddzialyFromDb);
                Oddzialy = new ObservableCollection<OddzialDto>(listaOddzialow);
            }
            catch (Exception e)
            {
                MessageBox.Show("Nie udało się załadować oddziałów.", "Error",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public async Task LoadItem(object objId)
        {
            try
            {
                Uczen uczenFromDb = null;
                var uczenId = Convert.ToInt32(objId);
                if (uczenId == default)
                    throw new ArgumentException("Przesłano nieprawidłowy identyfikator obiektu");

                using (var scope = _serviceProvider.CreateScope())
                {
                    var repository = scope.ServiceProvider.GetRequiredService<IPlacowkaRepository>();
                    uczenFromDb = await repository.Uczniowie.GetByIdAsync(uczenId);
                }
                _ = uczenFromDb ??
                    throw new DataNotFoundException(
                        $"Nie znaleziono ucznia o podanym identyfikatorze ({objId})");

                base.DisplayName = BaseResources.EdycjaUcznia;
                base.AddItemName = BaseResources.SaveItem;


                _signal.SendMessage(this, $"Widok: {DisplayName}");

                Item = _mapper.Map<UczenDto>(uczenFromDb);
                //Item.Adres ??= new AdresDto();
                foreach (var prop in this.GetType().GetProperties())
                    this.OnPropertyChanged(prop.Name);
            }
            catch (Exception e)
            {
                MessageBox.Show($"Nie udało się zainicjalizować obiektu. {e.Message}",
                    "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        #endregion

        #region Helpers
        public override void Dispose()
        {
            base.Dispose();
        }
        #endregion
    }
}
