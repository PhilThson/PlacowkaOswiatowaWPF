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
    public class WszystkieUmowyViewModel : ItemsCollectionViewModel<UmowaDto>, ILoadable
    {
        #region Pola i komendy
        protected override Type ItemToCreateType => typeof(NowaUmowaViewModel);
        protected override Type EntityType => typeof(Umowa);
        #endregion

        #region Konstruktor
        public WszystkieUmowyViewModel(IServiceProvider serviceProvider, IMapper mapper)
            : base(serviceProvider, mapper, BaseResources.WszystkieUmowy, BaseResources.DodajUmowe)
        {

        }
        #endregion

        #region Incjacja
        public async Task LoadAsync()
        {
            try
            {
                List<Umowa> umowy = new List<Umowa>();
                using (var scope = _serviceProvider.CreateScope())
                {
                    var repository = scope.ServiceProvider.GetRequiredService<IPlacowkaRepository>();
                    umowy = await repository.Umowy.GetAllAsync();
                }
                AllList = _mapper.Map<List<UmowaDto>>(umowy);
                List = new ObservableCollection<UmowaDto>(AllList);
            }
            catch (Exception e)
            {
                MessageBox.Show($"Nie udało się pobrać umów. {e.Message}", "Błąd",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        #endregion

        #region Metody
        protected override void Update() => Load();

        protected override void Load()
        {
            List<Umowa> umowy = new List<Umowa>();
            using (var scope = _serviceProvider.CreateScope())
            {
                var repository = scope.ServiceProvider.GetRequiredService<IPlacowkaRepository>();
                umowy = repository.Umowy.GetAll();
            }
            AllList = _mapper.Map<IEnumerable<UmowaDto>>(umowy);
            List = new ObservableCollection<UmowaDto>(AllList);
        }

        protected override Func<UmowaDto, string> SetOrderBySelector() =>
            SelectedOrderBy switch
            {
                nameof(UmowaDto.Pracodawca.Nazwa) => p => p.Pracodawca.Nazwa,
                nameof(UmowaDto.Pracownik.Imie) => p => p.Pracownik.Imie,
                nameof(UmowaDto.Pracownik.Nazwisko) => p => p.Pracownik.Nazwisko,
                nameof(UmowaDto.Stanowisko) => p => p.Stanowisko?.Opis,
                nameof(UmowaDto.Etat) => p => p.Etat?.Opis,
                _ => p => string.Empty
            };

        protected override Func<UmowaDto, bool> SetFilterPredicate() =>
            SelectedFilter switch
            {
                nameof(UmowaDto.Pracodawca.Nazwa) =>
                    u => u.Pracodawca.Nazwa?.Contains(SearchPhrase,
                        StringComparison.InvariantCultureIgnoreCase) ?? false,
                nameof(UmowaDto.Pracownik.Imie) =>
                    p => p.Pracownik.Imie?.Contains(SearchPhrase,
                        StringComparison.InvariantCultureIgnoreCase) ?? false,
                nameof(UmowaDto.Pracownik.Nazwisko) =>
                    p => p.Pracownik.Nazwisko?.Contains(SearchPhrase,
                        StringComparison.InvariantCultureIgnoreCase) ?? false,
                nameof(UmowaDto.Stanowisko) =>
                    p => p.Stanowisko?.Opis?.Contains(SearchPhrase,
                        StringComparison.InvariantCultureIgnoreCase) ?? false,
                nameof(UmowaDto.Etat) =>
                    p => p.Etat?.Opis?.Contains(SearchPhrase,
                        StringComparison.InvariantCultureIgnoreCase) ?? false,
                _ => p => true
            };

        protected override List<KeyValuePair<string, string>> SetListOfItemsFilter() =>
            new List<KeyValuePair<string, string>>()
            {
                new KeyValuePair<string, string>(nameof(UmowaDto.Pracodawca.Nazwa), "Nazwa pracodawcy"),
                new KeyValuePair<string, string>(nameof(UmowaDto.Pracownik.Imie), "Imię pracownika"),
                new KeyValuePair<string, string>(nameof(UmowaDto.Pracownik.Nazwisko), "Nazwisko pracownika"),
                new KeyValuePair<string, string>(nameof(UmowaDto.Stanowisko), "Stanowisko"),
                new KeyValuePair<string, string>(nameof(UmowaDto.Etat), "Etat"),
            };

        protected override List<KeyValuePair<string, string>> SetListOfItemsOrderBy() =>
            new List<KeyValuePair<string, string>>()
            {
                new KeyValuePair<string, string>(nameof(UmowaDto.Pracodawca.Nazwa), "Nazwa pracodawcy"),
                new KeyValuePair<string, string>(nameof(UmowaDto.Pracownik.Imie), "Imię pracownika"),
                new KeyValuePair<string, string>(nameof(UmowaDto.Pracownik.Nazwisko), "Nazwisko pracownika"),
                new KeyValuePair<string, string>(nameof(UmowaDto.Stanowisko), "Stanowisko"),
                new KeyValuePair<string, string>(nameof(UmowaDto.Etat), "Etat"),
            };
        #endregion

    }
}
