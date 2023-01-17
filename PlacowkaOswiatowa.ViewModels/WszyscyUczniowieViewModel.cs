using AutoMapper;
using PlacowkaOswiatowa.Domain.DTOs;
using PlacowkaOswiatowa.Domain.Interfaces.CommonInterfaces;
using PlacowkaOswiatowa.Domain.Interfaces.RepositoryInterfaces;
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
    public class WszyscyUczniowieViewModel : ItemsCollectionViewModel<UczenDto>, ILoadable
    {
        #region Pola, właściwości, komendy
        protected override Type ItemToCreateType => typeof(NowyUczenViewModel);
        //public ICollectionView StudentsCollectionView { get; set; }
        #endregion

        #region Konstruktor
        public WszyscyUczniowieViewModel(IPlacowkaRepository repository, IMapper mapper)
            : base(repository, mapper, BaseResources.WszyscyUczniowie, BaseResources.DodajUcznia)
        {
            //StudentsCollectionView = CollectionViewSource.GetDefaultView(List);
        }
        #endregion

        #region Inicjacja
        public async Task LoadAsync()
        {
            try
            {
                var uczniowieFromDb = await _repository.Uczniowie.GetAllAsync();
                AllList = _mapper.Map<List<UczenDto>>(uczniowieFromDb);

                List = new ObservableCollection<UczenDto>(AllList);
            }

            catch(Exception)
            {
                MessageBox.Show("Nie udało się pobrać listy uczniów.", "Błąd",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        #endregion

        #region Metody
        protected override void Load()
        {
            List = new ObservableCollection<UczenDto>
                (
                    _mapper.Map<IEnumerable<UczenDto>>
                        (
                            _repository.Uczniowie.GetAll()
                        )
                );
        }

        protected override void Update() => Load();

        protected override void OrderBy()
        {
            if (string.IsNullOrEmpty(SelectedFilter))
                return;

            switch (SelectedOrderBy)
            {
                case nameof(UczenDto.Imie):
                    List = new ObservableCollection<UczenDto>(OrderDescending
                        ? List.OrderByDescending(item => item.Imie)
                        : List.OrderBy(item => item.Imie));
                    break;
            };
        }

        protected override void Filter()
        {
            if (string.IsNullOrEmpty(SearchPhrase))
            {
                List = new ObservableCollection<UczenDto>(AllList);
                return;
            }

            switch (SelectedFilter)
            {
                case nameof(UczenDto.Imie):
                    List = new ObservableCollection<UczenDto>(AllList
                        .Where(item => item.Imie?.Contains(SearchPhrase) ?? false));
                    break;
                default:
                    List = new ObservableCollection<UczenDto>(AllList);
                    break;
            };

            OrderBy();
        }

        protected override List<KeyValuePair<string, string>> SetListOfItemsFilter() =>
            new List<KeyValuePair<string, string>>()
            {
                new KeyValuePair<string, string>(nameof(UczenDto.Imie), "Imię")
            };

        protected override List<KeyValuePair<string, string>> SetListOfItemsOrderBy() =>
            new List<KeyValuePair<string, string>>()
            {
                new KeyValuePair<string, string>(nameof(UczenDto.Imie), "Imię")
            };

        #endregion
    }
}
