using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
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

namespace PlacowkaOswiatowa.ViewModels
{
    public class WszyscyPracownicyViewModel : ItemsCollectionViewModel<PracownikDto>, ILoadable
    {
        #region Pola i komendy
        ILogger<WszyscyPracownicyViewModel> _logger;
        protected override Type ItemToCreateType => typeof(NowyPracownikViewModel);
        protected override Type EntityType => typeof(Pracownik);
        #endregion

        #region Konstruktor
        public WszyscyPracownicyViewModel(IServiceProvider serviceProvider, IMapper mapper, 
            ILogger<WszyscyPracownicyViewModel> logger)
            : base(serviceProvider, mapper, BaseResources.WszyscyPracownicy, BaseResources.DodajPracownika)
        {
            _signal.RequestRefreshEmployeesView += Update;
            _logger = logger;
        }
        #endregion

        #region Incjacja
        public async Task LoadAsync()
        {
            var pracownicy = new List<Pracownik>();
            using (var scope = _serviceProvider.CreateScope())
            {
                var repository = scope.ServiceProvider.GetRequiredService<IPlacowkaRepository>();
                pracownicy = await repository.Pracownicy.GetAllAsync();
            }
            AllList = _mapper.Map<IEnumerable<PracownikDto>>(pracownicy);
            List = new ObservableCollection<PracownikDto>(AllList);
            _logger.LogInformation("Pobrano wszystkich pracowników");
        }
        #endregion

        #region Metody
        protected override void Update() => Load();

        protected override void Load()
        {
            try
            {
                var pracownicy = new List<Pracownik>();
                using (var scope = _serviceProvider.CreateScope())
                {
                    var repository = scope.ServiceProvider.GetRequiredService<IPlacowkaRepository>();
                    pracownicy = repository.Pracownicy.GetAll();
                }
                AllList = _mapper.Map<List<PracownikDto>>(pracownicy);
                List = new ObservableCollection<PracownikDto>(AllList);
            }
            catch(Exception e)
            {
                _logger.LogError("Błąd podczas odświeżania listy pracowników: {error}", e.Message);
            }
        }
        public override void Dispose()
        {
            _signal.RequestRefreshEmployeesView -= Update;
            base.Dispose();
        }

        #endregion

        #region Filtrowanie, sortowanie
        protected override Func<PracownikDto, string> SetOrderBySelector() =>
            SelectedOrderBy switch
            {
                nameof(PracownikDto.Imie) => p => p.Imie,
                nameof(PracownikDto.Nazwisko) => p => p.Nazwisko,
                nameof(PracownikDto.Stanowisko) => p => p.Stanowisko?.Opis,
                nameof(PracownikDto.Etat) => p => p.Etat?.Opis,
                _ => p => string.Empty
            };

        protected override Func<PracownikDto, bool> SetFilterPredicate() =>
            SelectedFilter switch
            {
                nameof(PracownikDto.Imie) =>
                    p => p.Imie?.Contains(SearchPhrase, 
                        StringComparison.InvariantCultureIgnoreCase) ?? false,
                nameof(PracownikDto.Nazwisko) =>
                    p => p.Nazwisko?.Contains(SearchPhrase, 
                        StringComparison.InvariantCultureIgnoreCase) ?? false,
                nameof(PracownikDto.Stanowisko) =>
                    p => p.Stanowisko?.Opis?.Contains(SearchPhrase, 
                        StringComparison.InvariantCultureIgnoreCase) ?? false,
                nameof(PracownikDto.Etat) =>
                    p => p.Etat?.Opis?.Contains(SearchPhrase, 
                        StringComparison.InvariantCultureIgnoreCase) ?? false,
                _ => p => true
            };

        protected override List<KeyValuePair<string, string>> SetListOfItemsFilter() =>
            new List<KeyValuePair<string, string>>()
            {
                new KeyValuePair<string, string>(nameof(PracownikDto.Imie), "Imię"),
                new KeyValuePair<string, string>(nameof(PracownikDto.Nazwisko), "Nazwisko"),
                new KeyValuePair<string, string>(nameof(PracownikDto.Stanowisko), "Stanowisko"),
                new KeyValuePair<string, string>(nameof(PracownikDto.Etat), "Etat"),
            };

        protected override List<KeyValuePair<string, string>> SetListOfItemsOrderBy() =>
            new List<KeyValuePair<string, string>>()
            {
                new KeyValuePair<string, string>(nameof(PracownikDto.Imie), "Imię"),
                new KeyValuePair<string, string>(nameof(PracownikDto.Nazwisko), "Nazwisko"),
                new KeyValuePair<string, string>(nameof(PracownikDto.Stanowisko), "Stanowisko"),
                new KeyValuePair<string, string>(nameof(PracownikDto.Etat), "Etat"),
            };
        #endregion
    }
}
