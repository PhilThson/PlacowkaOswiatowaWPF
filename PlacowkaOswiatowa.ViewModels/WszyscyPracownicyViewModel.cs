using AutoMapper;
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
    public class WszyscyPracownicyViewModel : ItemsCollectionViewModel<PracownikDto>, ILoadable
    {
        #region Pola i komendy
        protected override Type ItemToCreateType => typeof(NowyPracownikViewModel);
        protected override Type EntityType => typeof(Pracownik);

        private readonly ISignalHub<ViewHandler> _signal;
        #endregion

        #region Konstruktor
        public WszyscyPracownicyViewModel(IPlacowkaRepository repository, IMapper mapper)
            : base(repository, mapper, BaseResources.WszyscyPracownicy, BaseResources.DodajPracownika)
        {
            _signal = SignalHub<ViewHandler>.Instance;
        }
        #endregion

        #region Incjacja
        public async Task LoadAsync()
        {
            try
            {
                var pracownicyFormDb = await _repository.Pracownicy.GetAllAsync();
                AllList = _mapper.Map<IEnumerable<PracownikDto>>(pracownicyFormDb);
                List = new ObservableCollection<PracownikDto>(AllList);
            }
            catch(Exception e)
            {
                MessageBox.Show($"Nie udało się pobrać pracowników. {e.Message}", "Błąd",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        #endregion

        #region Metody
        protected override void Update() => Load();

        protected override void Load()
        {
            AllList = _mapper.Map<IEnumerable<PracownikDto>>(_repository.Pracownicy.GetAll());
            List = new ObservableCollection<PracownikDto>(AllList);
        }

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
