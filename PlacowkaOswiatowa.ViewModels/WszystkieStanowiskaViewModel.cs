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
    public class WszystkieStanowiskaViewModel : ItemsCollectionViewModel<StanowiskoDto>, ILoadable
    {

        #region Pola i komendy
        protected override Type ItemToCreateType => typeof(NoweStanowiskoViewModel);
        #endregion

        #region Konstruktor
        public WszystkieStanowiskaViewModel(IPlacowkaRepository repository, IMapper mapper)
            : base(repository, mapper, BaseResources.WszystkiePrzedmioty)
        { 
        }
        #endregion

        #region Incjacja
        public async Task LoadAsync()
        {
            try
            {
                var stanowiskaFromDb = await _repository.Stanowiska.GetAllAsync();
                AllList = _mapper.Map<List<StanowiskoDto>>(stanowiskaFromDb);
                List = new ObservableCollection<StanowiskoDto>(AllList);
            }
            catch (Exception)
            {
                MessageBox.Show("Nie udało się pobrać stanowisk.", "Błąd",
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
            AllList = _mapper.Map<List<StanowiskoDto>>(_repository.Stanowiska.GetAll());
            List = new ObservableCollection<StanowiskoDto>(AllList);
        }
        #endregion
    }
}
