using PlacowkaOswiatowa.Domain.Interfaces.CommonInterfaces;
using PlacowkaOswiatowa.Domain.Interfaces.RepositoryInterfaces;
using PlacowkaOswiatowa.Domain.Models;
using PlacowkaOswiatowa.Domain.Resources;
using PlacowkaOswiatowa.ViewModels.Abstract;
using System.Collections.ObjectModel;
using System;
using System.Threading.Tasks;
using System.Windows;

namespace PlacowkaOswiatowa.ViewModels
{
    public class WszystkiePrzedmiotyViewModel : ItemsCollectionViewModel<Przedmiot>, ILoadable
    {

        #region Pola, właściwości, komendy
        protected override Type ItemToCreateType => typeof(NowyPrzedmiotViewModel);
        #endregion


        #region Konstruktor
        public WszystkiePrzedmiotyViewModel(IPlacowkaRepository repository)
            : base(BaseResources.WszystkiePrzedmioty, repository)
        { }
        #endregion

        #region Incjacja
        public async Task LoadAsync()
        {
            try
            {
                var listaPrzedmiotow = await _repository.Przedmioty.GetAllAsync();

                List = new ObservableCollection<Przedmiot>(listaPrzedmiotow);
            }
            catch (Exception)
            {
                MessageBox.Show("Nie udało się pobrać przedmiotów.", "Błąd",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        #endregion

        #region Metody
        public override void Update()
        {
            Load();
        }

        //Ta metoda zostanie prawdopodobnie podpięta pod przycisk 'Odswież'
        public override void Load()
        {
            List = new ObservableCollection<Przedmiot>
                (
                    _repository.Przedmioty.GetAll()
                );
        }
        #endregion
    }
}
