using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using PlacowkaOswiatowa.Domain.DTOs;
using PlacowkaOswiatowa.Domain.Helpers;
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
    public class WszyscyUczniowieViewModel : ItemsCollectionViewModel<UczenDto>, ILoadable
    {
        #region Pola, właściwości, komendy
        protected override Type ItemToCreateType => typeof(NowyUczenViewModel);
        protected override Type EntityType => typeof(Uczen);

        private readonly ISignalHub<ViewHandler> _signal;
        //public ICollectionView StudentsCollectionView { get; set; }
        #endregion

        #region Konstruktor
        public WszyscyUczniowieViewModel(IServiceProvider serviceProvider, IMapper mapper)
            : base(serviceProvider, mapper, BaseResources.WszyscyUczniowie, BaseResources.DodajUcznia)
        {
            _signal = SignalHub<ViewHandler>.Instance;
            //StudentsCollectionView = CollectionViewSource.GetDefaultView(List);
        }
        #endregion

        #region Inicjacja
        public async Task LoadAsync()
        {
            try
            {
                var uczniowie = new List<Uczen>();
                using (var scope = _serviceProvider.CreateScope())
                {
                    var repository = scope.ServiceProvider.GetRequiredService<IPlacowkaRepository>();
                    uczniowie = await repository.Uczniowie.GetAllAsync();
                }
                AllList = _mapper.Map<List<UczenDto>>(uczniowie);
                List = new ObservableCollection<UczenDto>(AllList);
            }

            catch(Exception e)
            {
                MessageBox.Show($"Nie udało się pobrać listy uczniów. {e.Message}", "Błąd",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        #endregion

        #region Metody
        protected override void Update() => Load();

        protected override void Load()
        {
            var uczniowie = new List<Uczen>();
            using (var scope = _serviceProvider.CreateScope())
            {
                var repository = scope.ServiceProvider.GetRequiredService<IPlacowkaRepository>();
                uczniowie = repository.Uczniowie.GetAll();
            }
            AllList = _mapper.Map<List<UczenDto>>(uczniowie);
            List = new ObservableCollection<UczenDto>(AllList);
        }

        protected override Func<UczenDto, string> SetOrderBySelector() =>
            SelectedOrderBy switch
            {
                nameof(UczenDto.Imie) => u => u.Imie,
                nameof(UczenDto.Nazwisko) => u => u.Nazwisko,
                nameof(UczenDto.Oddzial.Nazwa) => u => u.Oddzial?.Nazwa,
                _ => u => string.Empty
            };

        protected override Func<UczenDto, bool> SetFilterPredicate() =>
            SelectedFilter switch
            {
                nameof(UczenDto.Imie) =>
                    u => u.Imie?.Contains(SearchPhrase, StringComparison.InvariantCultureIgnoreCase) ?? false,
                nameof(UczenDto.Nazwisko) =>
                    u => u.Nazwisko?.Contains(SearchPhrase, StringComparison.InvariantCultureIgnoreCase) ?? false,
                nameof(UczenDto.Oddzial.Nazwa) =>
                    u => u.Oddzial?.Nazwa?.Contains(SearchPhrase, StringComparison.InvariantCultureIgnoreCase) ?? false,
                _ => u => true
            };

        protected override List<KeyValuePair<string, string>> SetListOfItemsFilter() =>
            new List<KeyValuePair<string, string>>()
            {
                new KeyValuePair<string, string>(nameof(UczenDto.Imie), "Imię"),
                new KeyValuePair<string, string>(nameof(UczenDto.Nazwisko), "Nazwisko"),
                new KeyValuePair<string, string>(nameof(UczenDto.Oddzial.Nazwa), "Oddział")
            };

        protected override List<KeyValuePair<string, string>> SetListOfItemsOrderBy() =>
            new List<KeyValuePair<string, string>>()
            {
                new KeyValuePair<string, string>(nameof(UczenDto.Imie), "Imię"),
                new KeyValuePair<string, string>(nameof(UczenDto.Nazwisko), "Nazwisko"),
                new KeyValuePair<string, string>(nameof(UczenDto.Oddzial.Nazwa), "Oddział"),
            };

        #endregion
    }
}
