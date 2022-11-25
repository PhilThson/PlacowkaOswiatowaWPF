﻿using AutoMapper;
using PlacowkaOswiatowa.Domain.DTOs;
using PlacowkaOswiatowa.Domain.Interfaces.CommonInterfaces;
using PlacowkaOswiatowa.Domain.Interfaces.RepositoryInterfaces;
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
        private readonly IMapper _mapper;
        //nie można bindować do modeli które nie implementują interfejsu
        //  INotifyPropertyChanged - może to powodować wycieki pamięci
        //chyba żeby zastosować wrappery, które będą implementowały ten interfejs
        //private ObservableCollection<PracownikDto> _pracownicy;
        //public IEnumerable<PracownikDto> Pracownicy => _pracownicy;
        //public ICommand DodajPracownikaCommand 
        //{
        //    get => new BaseCommand(DodajNowegoPracownika);
        //}

        protected override Type ItemToCreateType => typeof(NowyPracownikViewModel);
        #endregion

        #region Konstruktor
        public WszyscyPracownicyViewModel(IPlacowkaRepository repository, IMapper mapper)
            : base(BaseResources.WszyscyPracownicy, repository)
        {
            _mapper = mapper;
        }
        #endregion

        #region Incjacja
        public async Task LoadAsync()
        {
            try
            {
                var pracownicyFormDb = await _repository.Pracownicy.GetAllAsync(
                    includeProperties: "Etat,Stanowisko,PracownikPracownicyAdresy");
                var listaPracownikow = _mapper.Map<IEnumerable<PracownikDto>>(pracownicyFormDb);

                List = new ObservableCollection<PracownikDto>(listaPracownikow);
                // 'List' jest właściwością w której setterze
                //powiadamiany jest interfejs że nastąpiły zmiany
            }
            catch(Exception)
            {
                MessageBox.Show("Nie udało się pobrać pracowników.", "Błąd",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        #endregion

        #region Helpers
        public override void Update()
        {
            Load();
        }

        //Ta metoda zostanie raczej podpięta pod przycisk 'Odswież'
        public override void Load()
        {
            List = new ObservableCollection<PracownikDto>
                (
                    _mapper.Map<IEnumerable<PracownikDto>>
                        (
                            _repository.Pracownicy.GetAll(
                                includeProperties: "Etat,Stanowisko,PracownikPracownicyAdresy")
                        )
                );
        }
        #endregion
    }
}
