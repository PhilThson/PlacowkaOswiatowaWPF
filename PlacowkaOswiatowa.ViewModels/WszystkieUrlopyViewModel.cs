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
    public class WszystkieUrlopyViewModel : ItemsCollectionViewModel<UrlopDto>, ILoadable
    {
        #region Pola, właściwości, komendy
        protected override Type ItemToCreateType => typeof(UrlopPracownikaViewModel);
        #endregion

        #region Konstruktor
        public WszystkieUrlopyViewModel(IServiceProvider serviceProvider, IMapper mapper)
            : base(serviceProvider, mapper, BaseResources.WszystkieUrlopy)
        {
        }
        #endregion

        #region Incjacja
        public async Task LoadAsync()
        {
            var urlopy = new List<Urlop>();
            using (var scope = _serviceProvider.CreateScope())
            {
                var repository = scope.ServiceProvider.GetRequiredService<IPlacowkaRepository>();
                urlopy = await repository.Urlopy.GetAllAsync();
            }
            AllList = _mapper.Map<List<UrlopDto>>(urlopy);
            List = new ObservableCollection<UrlopDto>(AllList);

        }
        #endregion

        #region Metody
        protected override void Update() => Load();

        protected override void Load()
        {
            try
            {
                var urlopy = new List<Urlop>();
                using (var scope = _serviceProvider.CreateScope())
                {
                    var repository = scope.ServiceProvider.GetRequiredService<IPlacowkaRepository>();
                    urlopy = repository.Urlopy.GetAll();
                }
                AllList = _mapper.Map<List<UrlopDto>>(urlopy);
                List = new ObservableCollection<UrlopDto>(AllList);
            }
            catch (Exception e)
            {
                MessageBox.Show($"Nie udało się pobrać urlopów. {e.Message}", "Błąd",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        #endregion

        #region Filtrowanie, sortowanie
        protected override Func<UrlopDto, string> SetOrderBySelector() =>
           SelectedOrderBy switch
           {
               nameof(UrlopDto.Pracownik.Nazwisko) => p => p.Pracownik.Nazwisko,
               nameof(UrlopDto.ZastepujacyPracownik) => p => p.ZastepujacyPracownik,
               nameof(UrlopDto.PrzyczynaUrlopu) => p => p.PrzyczynaUrlopu,
               nameof(UrlopDto.PoczatekUrlopu) => p => p.PoczatekUrlopu.ToShortDateString(),
               _ => p => string.Empty
           };

        protected override Func<UrlopDto, bool> SetFilterPredicate() =>
            SelectedFilter switch
            {
                nameof(UrlopDto.Pracownik.Nazwisko) =>
                    p => p.Pracownik?.Nazwisko?.Contains(SearchPhrase,
                        StringComparison.InvariantCultureIgnoreCase) ?? false,
                nameof(UrlopDto.ZastepujacyPracownik) =>
                    p => p.ZastepujacyPracownik?.Contains(SearchPhrase,
                        StringComparison.InvariantCultureIgnoreCase) ?? false,
                nameof(UrlopDto.PrzyczynaUrlopu) =>
                    p => p.PrzyczynaUrlopu?.Contains(SearchPhrase,
                        StringComparison.InvariantCultureIgnoreCase) ?? false,
                nameof(UrlopDto.PoczatekUrlopu) =>
                    p => p.PoczatekUrlopu.ToShortDateString()?.Contains(SearchPhrase,
                        StringComparison.InvariantCultureIgnoreCase) ?? false,
                _ => p => true
            };

        protected override List<KeyValuePair<string, string>> SetListOfItemsFilter() =>
            new List<KeyValuePair<string, string>>()
            {
                new KeyValuePair<string, string>(nameof(UrlopDto.Pracownik.Nazwisko), "Nazwisko pracownika"),
                new KeyValuePair<string, string>(nameof(UrlopDto.ZastepujacyPracownik), "Zastępujący pracownik"),
                new KeyValuePair<string, string>(nameof(UrlopDto.PrzyczynaUrlopu), "Przyczyna urlopu"),
                new KeyValuePair<string, string>(nameof(UrlopDto.PoczatekUrlopu), "Początek urlopu"),
            };

        protected override List<KeyValuePair<string, string>> SetListOfItemsOrderBy() =>
            new List<KeyValuePair<string, string>>()
            {
                new KeyValuePair<string, string>(nameof(UrlopDto.Pracownik.Nazwisko), "Nazwisko pracownika"),
                new KeyValuePair<string, string>(nameof(UrlopDto.ZastepujacyPracownik), "Zastępujący pracownik"),
                new KeyValuePair<string, string>(nameof(UrlopDto.PrzyczynaUrlopu), "Przyczyna urlopu"),
                new KeyValuePair<string, string>(nameof(UrlopDto.PoczatekUrlopu), "Początek urlopu"),
            };
        #endregion
    }
}
