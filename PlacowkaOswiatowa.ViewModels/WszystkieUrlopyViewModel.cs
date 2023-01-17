using AutoMapper;
using PlacowkaOswiatowa.Domain.Interfaces.CommonInterfaces;
using PlacowkaOswiatowa.Domain.Interfaces.RepositoryInterfaces;
using PlacowkaOswiatowa.Domain.Models;
using PlacowkaOswiatowa.Domain.Resources;
using PlacowkaOswiatowa.ViewModels.Abstract;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;

namespace PlacowkaOswiatowa.ViewModels
{
    public class WszystkieUrlopyViewModel : ItemsCollectionViewModel<Urlop>, ILoadable
    {
        #region Pola, właściwości, komendy
        protected override Type ItemToCreateType => typeof(UrlopPracownikaViewModel);
        #endregion


        #region Konstruktor
        public WszystkieUrlopyViewModel(IPlacowkaRepository repository, IMapper mapper)
            : base(repository, mapper, BaseResources.WszystkieUrlopy)
        { 
        }
        #endregion

        #region Incjacja
        public async Task LoadAsync()
        {
            try
            {
                var listaUrlopow = await _repository.Urlopy.GetAllAsync();

                List = new ObservableCollection<Urlop>(listaUrlopow);
            }
            catch (Exception)
            {
                MessageBox.Show("Nie udało się pobrać urlopów.", "Błąd",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        #endregion

        #region Metody
        protected override void Update()
        {
            Load();
        }

        protected override void Load()
        {
            List = new ObservableCollection<Urlop>
                (
                    _repository.Urlopy.GetAll()
                );
        }
        #endregion
    }
}
