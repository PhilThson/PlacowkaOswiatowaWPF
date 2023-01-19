using PlacowkaOswiatowa.Domain.Interfaces.CommonInterfaces;
using PlacowkaOswiatowa.Domain.Interfaces.RepositoryInterfaces;
using PlacowkaOswiatowa.Domain.Resources;
using PlacowkaOswiatowa.ViewModels.Abstract;
using System.Collections.ObjectModel;
using System;
using System.Threading.Tasks;
using System.Windows;
using AutoMapper;
using PlacowkaOswiatowa.Domain.DTOs;
using System.Collections.Generic;

namespace PlacowkaOswiatowa.ViewModels
{
    public class WszystkiePrzedmiotyViewModel : ItemsCollectionViewModel<PrzedmiotDto>, ILoadable
    {

        #region Pola, właściwości, komendy
        protected override Type ItemToCreateType => typeof(NowyPrzedmiotViewModel);
        #endregion


        #region Konstruktor
        public WszystkiePrzedmiotyViewModel(IPlacowkaRepository repository, IMapper mapper)
            : base(repository, mapper, BaseResources.WszystkiePrzedmioty)
        { 
        }
        #endregion

        #region Incjacja
        public async Task LoadAsync()
        {
            try
            {
                var przedmiotyFromDb = await _repository.Przedmioty.GetAllAsync();
                AllList = _mapper.Map<List<PrzedmiotDto>>(przedmiotyFromDb);
                List = new ObservableCollection<PrzedmiotDto>(AllList);
            }
            catch (Exception)
            {
                MessageBox.Show("Nie udało się pobrać przedmiotów.", "Błąd",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        #endregion

        #region Metody
        protected override void Update()
        {
            Load();
        }

        //Ta metoda zostanie prawdopodobnie podpięta pod przycisk 'Odswież'
        protected override void Load()
        {
            AllList = _mapper.Map<List<PrzedmiotDto>>(_repository.Przedmioty.GetAll());
            List = new ObservableCollection<PrzedmiotDto>(AllList);
        }
        #endregion
    }
}
