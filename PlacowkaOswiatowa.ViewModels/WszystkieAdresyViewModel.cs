using AutoMapper;
using PlacowkaOswiatowa.Domain.DTOs;
using PlacowkaOswiatowa.Domain.Interfaces.CommonInterfaces;
using PlacowkaOswiatowa.Domain.Resources;
using PlacowkaOswiatowa.ViewModels.Abstract;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;
using System;
using Microsoft.Extensions.DependencyInjection;
using PlacowkaOswiatowa.Domain.Interfaces.RepositoryInterfaces;
using PlacowkaOswiatowa.Domain.Models;
using System.Linq;
using Microsoft.Extensions.Logging;

namespace PlacowkaOswiatowa.ViewModels
{
    public class WszystkieAdresyViewModel : ItemsCollectionViewModel<AdresDto>, ILoadable
    {
        #region Pola i komendy
        protected override Type ItemToCreateType => typeof(NowyAdresViewModel);
        private readonly ILogger<WszystkieAdresyViewModel> _logger;
        #endregion


        #region Konstruktor
        public WszystkieAdresyViewModel(IServiceProvider serviceProvider, IMapper mapper, 
            ILogger<WszystkieAdresyViewModel> logger)
            : base(serviceProvider, mapper, BaseResources.WszystkieAdresy)
        {
            _logger = logger;
        }
        #endregion

        #region Incjacja
        public async Task LoadAsync()
        {
            var adresy = new List<Adres>();
            using (var scope = _serviceProvider.CreateScope())
            {
                var repository = scope.ServiceProvider.GetRequiredService<IPlacowkaRepository>();
                adresy = await repository.Adresy.GetAllAsync(
                    includeProperties: "Panstwo,Miejscowosc,Ulica");
            }
            AllList = _mapper.Map<List<AdresDto>>(adresy);
            List = new ObservableCollection<AdresDto>(AllList);
        }
        #endregion

        #region Metody
        protected override void Update() => Load();

        protected override void Load()
        {
            try
            {
                var adresy = new List<Adres>();
                using (var scope = _serviceProvider.CreateScope())
                {
                    var repository = scope.ServiceProvider.GetRequiredService<IPlacowkaRepository>();
                    adresy = repository.Adresy.GetAll(
                        includeProperties: "Panstwo,Miejscowosc,Ulica").ToList();
                }
                AllList = _mapper.Map<List<AdresDto>>(adresy);
                List = new ObservableCollection<AdresDto>(AllList);
            }
            catch (Exception e)
            {
                _logger.LogError("Błąd odświeżania listy adresów: {error}", e.Message);

                MessageBox.Show("Nie udało się pobrać ocen.", "Błąd",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        #endregion

        #region Filtrowanie, sortowanie
        protected override Func<AdresDto, string> SetOrderBySelector() =>
           SelectedOrderBy switch
           {
               nameof(AdresDto.Panstwo) => p => p.Panstwo,
               nameof(AdresDto.Miejscowosc) => p => p.Miejscowosc,
               nameof(AdresDto.Ulica) => p => p.Ulica,
               nameof(AdresDto.KodPocztowy) => p => p.KodPocztowy,
               _ => p => string.Empty
           };

        protected override Func<AdresDto, bool> SetFilterPredicate() =>
            SelectedFilter switch
            {
                nameof(AdresDto.Panstwo) =>
                    p => p.Panstwo?.Contains(SearchPhrase,
                        StringComparison.InvariantCultureIgnoreCase) ?? false,
                nameof(AdresDto.Miejscowosc) =>
                    p => p.Miejscowosc?.Contains(SearchPhrase,
                        StringComparison.InvariantCultureIgnoreCase) ?? false,
                nameof(AdresDto.Ulica) =>
                    p => p.Ulica?.Contains(SearchPhrase,
                        StringComparison.InvariantCultureIgnoreCase) ?? false,
                nameof(AdresDto.KodPocztowy) =>
                    p => p.KodPocztowy?.Contains(SearchPhrase,
                        StringComparison.InvariantCultureIgnoreCase) ?? false,
                _ => p => true
            };

        protected override List<KeyValuePair<string, string>> SetListOfItemsFilter() =>
            new List<KeyValuePair<string, string>>()
            {
                new KeyValuePair<string, string>(nameof(AdresDto.Panstwo), "Państwo"),
                new KeyValuePair<string, string>(nameof(AdresDto.Miejscowosc), "Miejscowość"),
                new KeyValuePair<string, string>(nameof(AdresDto.Ulica), "Ulica"),
                new KeyValuePair<string, string>(nameof(AdresDto.KodPocztowy), "Kod pocztowy"),
            };

        protected override List<KeyValuePair<string, string>> SetListOfItemsOrderBy() =>
            new List<KeyValuePair<string, string>>()
            {
                new KeyValuePair<string, string>(nameof(AdresDto.Panstwo), "Państwo"),
                new KeyValuePair<string, string>(nameof(AdresDto.Miejscowosc), "Miejscowość"),
                new KeyValuePair<string, string>(nameof(AdresDto.Ulica), "Ulica"),
                new KeyValuePair<string, string>(nameof(AdresDto.KodPocztowy), "Kod pocztowy"),
            };
        #endregion
    }
}