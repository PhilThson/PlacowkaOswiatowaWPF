using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using PlacowkaOswiatowa.Domain.DTOs;
using PlacowkaOswiatowa.Domain.Exceptions;
using PlacowkaOswiatowa.Domain.Helpers;
using PlacowkaOswiatowa.Domain.Interfaces.CommonInterfaces;
using PlacowkaOswiatowa.Domain.Interfaces.RepositoryInterfaces;
using PlacowkaOswiatowa.Domain.Models;
using PlacowkaOswiatowa.Domain.Resources;
using PlacowkaOswiatowa.ViewModels.Abstract;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace PlacowkaOswiatowa.ViewModels
{
    public class NowyPracownikViewModel : OneToManyViewModel<CreatePracownikDto, AdresDto>, 
        IEditable
    {
        #region Pola prywatne
        private readonly ILogger<NowyPracownikViewModel> _logger;
        private readonly ISignalHub _signal;
        private readonly Dictionary<Guid, int> _selectedItemsToEdit;
        #endregion

        #region Konstruktor
        public NowyPracownikViewModel(IServiceProvider serviceProvider, IMapper mapper, 
            ILogger<NowyPracownikViewModel> logger)
            : base(serviceProvider, mapper, BaseResources.DodajPracownika,
                  null, BaseResources.DodajAdres, BaseResources.EdycjaAdresu)
        {
            Item = new CreatePracownikDto();
            _signal = SignalHub.Instance;
            _logger = logger;
            _selectedItemsToEdit = new Dictionary<Guid, int>();
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
                    OnPropertyChanged();
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
                    OnPropertyChanged();
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
                    OnPropertyChanged();
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
                    OnPropertyChanged();
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
                    OnPropertyChanged();
                }
            }
        }
        #endregion

        #region Pola i właściwości Pracownika
        public string NrTelefonu
        {
            get => Item.NrTelefonu;
            set
            {
                if (value != Item.NrTelefonu)
                {
                    Item.NrTelefonu = value;
                    OnPropertyChanged();
                }
            }
        }
        public string Email
        {
            get => Item.Email;
            set
            {
                if (value != Item.Email)
                {
                    Item.Email = value;
                    OnPropertyChanged();
                }
            }
        }

        #endregion

        #region Metody

        protected override async Task<bool> SaveAsync()
        {
            try
            {
                var pracownik = _mapper.Map<Pracownik>(Item);

                using (var scope = _serviceProvider.CreateScope())
                {
                    var repository = scope.ServiceProvider.GetRequiredService<IPlacowkaRepository>();

                    if(pracownik.Id != default)
                    {
                        var pracownikById = await repository.Pracownicy.GetByIdAsync(pracownik.Id, false);
                        if (pracownikById == pracownik)
                            throw new DataValidationException("Nie dokonano zmian");
                    }

                    var pracownikByPesel = await repository.Pracownicy.GetPracownikByPeselAsync(pracownik.Pesel);
                    
                    if (pracownikByPesel is not null)
                        if (pracownikByPesel.Id != pracownik.Id)
                            throw new DataValidationException("Pracownik o podanym numerze PESEL już istnieje");
                    
                    if(AllList?.Count > 0)
                    {
                        foreach(var adresId in AllList.Select(a => a.Id))
                        {
                            if (Item.Adresy?.Any(a => a.Id == adresId) == true)
                                continue;

                            pracownik.PracownikPracownicyAdresy
                                .Add(new PracownicyAdresy { AdresId = (int)adresId });
                        }
                    }

                    if (pracownik.Id == default)
                        await repository.AddAsync(pracownik);
                    else
                        repository.Update(pracownik);
                                        
                    await repository.SaveAsync();
                }

                MessageBox.Show("Zapisano pracownika!", "Sukces",
                    MessageBoxButton.OK, MessageBoxImage.Information);

                _signal.RaiseRequestRefreshEmployeesView();

                return true;
            }
            catch (DataValidationException e)
            {
                MessageBox.Show(e.Message, "Uwaga",
                    MessageBoxButton.OK, MessageBoxImage.Warning);

                _logger.LogWarning(
                    "Wystąpił błąd podczas tworzenia/edycji pracownika: {error}",
                    e.Message);
            }
            catch (Exception e)
            {
                MessageBox.Show($"Nie udało się dodać pracownika. {e.Message}", "Error",
                    MessageBoxButton.OK, MessageBoxImage.Error);

                _logger.LogError(
                    "Wystąpił błąd podczas tworzenia/edycji pracownika: {error}",
                    e.Message);
            }
            return false;
        }

        protected override void CheckRequiredProperties() =>
            BaseCheckRequiredProperties(
                nameof(Imie),
                nameof(Nazwisko),
                nameof(DataUrodzenia),
                nameof(Pesel));

        #endregion

        #region Komendy powiązanej listy wszystkich elementów
        protected override void ShowAddManyView()
        {
            var viewHandler = new ViewHandler(typeof(NowyAdresViewModel), isModal: true);
            var listenerId = Guid.NewGuid();
            _signal.AddAddressCreatedListener(listenerId, this.OnAddressCreated);
            _signal.RaiseCreateView(listenerId, viewHandler);
        }

        protected override void ShowEditManyView(object itemId)
        {
            try
            {
                if (itemId is null)
                    throw new ArgumentNullException(nameof(itemId),
                        "Nie wybrano rekordu do edycji");

                var selectedAdresId = Convert.ToInt32(itemId);
                if(selectedAdresId == default)
                    throw new DataValidationException("Błąd odczytu wybranego adresu");

                var viewHandler = 
                    new ViewHandler(typeof(NowyAdresViewModel), selectedAdresId, true);

                var selectedItemIndex = AllList.IndexOf(SelectedItem);
                if (_selectedItemsToEdit.ContainsValue(selectedItemIndex))
                    throw new DataValidationException("Wybrany element jest już edytowany");

                var listenerId = Guid.NewGuid();
                _selectedItemsToEdit.Add(listenerId, selectedItemIndex);
                _signal.AddAddressCreatedListener(listenerId, this.OnAddressCreated);
                _signal.RaiseCreateView(listenerId, viewHandler);
            }
            catch(Exception e)
            {
                MessageBox.Show(e.Message, "Error", 
                    MessageBoxButton.OK, MessageBoxImage.Error);

                _logger.LogWarning(
                    "Wystąpił błąd podczas tworzenia widoku: {error}",
                    e.Message);
            }
        }

        //obsługa zdarzenia utworzenia adresu
        private void OnAddressCreated(AdresDto createdAddress, Guid listnerId)
        {
            AllList ??= new ObservableCollection<AdresDto>();

            if (_selectedItemsToEdit.ContainsKey(listnerId))
                AllList[_selectedItemsToEdit[listnerId]] = createdAddress;
            else
                AllList.Add(createdAddress);
        }
        #endregion

        #region Inicjacja do edycji
        public async Task LoadItem(object objId)
        {
            try
            {
                var pracownikId = Convert.ToInt32(objId);
                if (pracownikId == default)
                    throw new ArgumentException("Przesłano nieprawidłowy identyfikator obiektu");

                Pracownik pracownik = null;

                using (var scope = _serviceProvider.CreateScope())
                {
                    var repository = scope.ServiceProvider.GetRequiredService<IPlacowkaRepository>();
                    pracownik = await repository.Pracownicy.GetByIdAsync(pracownikId);
                }
                _ = pracownik ?? throw new DataNotFoundException(
                        $"Nie znaleziono pracownika o podanym identyfikatorze ({pracownikId})");

                base.DisplayName = BaseResources.EdycjaPracownika;
                base.AddItemName = BaseResources.SaveItem;

                //Dodatkowo wysłanie wiadomości z nowym statusem
                _signal.SendMessage(this, $"Widok: {DisplayName}");

                Item = _mapper.Map<CreatePracownikDto>(pracownik);
                //aby nie wyświetlać listy jeżeli jest pusta
                if(Item.Adresy?.Count > 0)
                    AllList = new ObservableCollection<AdresDto>(Item.Adresy);

                foreach (var prop in this.GetType().GetProperties())
                    this.OnPropertyChanged(prop.Name);
            }
            catch (Exception e)
            {
                MessageBox.Show($"Nie udało się zainicjalizować obiektu. {e.Message}",
                    "Error", MessageBoxButton.OK, MessageBoxImage.Error);

                _logger.LogError(
                    "Błąd podczas inicjalizacji obiektu do edycji: {error}",
                    e.Message);
            }
        }
        #endregion

        #region Disposing

        public override void Dispose()
        {
            //disposing: anulowanie subskrybcji do eventów pochodzących z globalnego zakresu
            // - wykonywane np. w przypadku zamkniecia zkladki
            //potrzebne w razie subskrybowania eventu z innej klasy
            //której czas życia trwa przez cały okres trwania aplikacji
            //potencjalne wycieki pamięci
            //this.PropertyChanged -= NowyPracownikViewModel_PropertyChanged;
            base.Dispose();
        }

        #endregion
    }
}
