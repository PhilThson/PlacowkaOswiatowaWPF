using AutoMapper;
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
    public class WszystkieUrlopyViewModel : ItemsCollectionViewModel<UrlopPracownikaDto>, ILoadable
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
                var urlopFromDb = await _repository.Urlopy.GetAllAsync();
                AllList = _mapper.Map<List<UrlopPracownikaDto>>(urlopFromDb);
                List = new ObservableCollection<UrlopPracownikaDto>(AllList);
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
            AllList = _mapper.Map<List<UrlopPracownikaDto>>(_repository.Urlopy.GetAll());
            List = new ObservableCollection<UrlopPracownikaDto>(AllList);
        }
        #endregion
    }
}
