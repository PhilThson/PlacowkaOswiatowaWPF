﻿using AutoMapper;
using PlacowkaOswiatowa.Domain.DTOs;
using PlacowkaOswiatowa.Domain.Helpers;
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
using static PlacowkaOswiatowa.Domain.Helpers.CommonExtensions;

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

            catch(Exception e)
            {
                MessageBox.Show($"Nie udało się pobrać listy uczniów. {e.Message}", "Błąd",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        #endregion

        #region Metody
        protected override void Load()
        {
            AllList = _mapper.Map<List<UczenDto>>(_repository.Uczniowie.GetAll());
            List = new ObservableCollection<UczenDto>(AllList);
        }

        protected override void Update() => Load();

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
