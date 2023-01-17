using PlacowkaOswiatowa.Domain.Interfaces.CommonInterfaces;
using PlacowkaOswiatowa.Domain.Interfaces.RepositoryInterfaces;
using PlacowkaOswiatowa.Domain.Models;
using PlacowkaOswiatowa.Domain.Resources;
using PlacowkaOswiatowa.ViewModels.Abstract;
using System.Collections.ObjectModel;
using System;
using System.Threading.Tasks;
using System.Windows;
using AutoMapper;

namespace PlacowkaOswiatowa.ViewModels
{
    public class WszystkieStanowiskaViewModel : ItemsCollectionViewModel<Stanowisko>, ILoadable
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
                var listaStanowisk = await _repository.Stanowiska.GetAllAsync();

                List = new ObservableCollection<Stanowisko>(listaStanowisk);
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
            List = new ObservableCollection<Stanowisko>
                (
                    _repository.Stanowiska.GetAll()
                );
        }
        #endregion
    }
}
