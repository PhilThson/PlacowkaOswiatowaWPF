using AutoMapper;
using PlacowkaOswiatowa.Domain.DTOs;
using PlacowkaOswiatowa.Domain.Interfaces.CommonInterfaces;
using PlacowkaOswiatowa.Domain.Interfaces.RepositoryInterfaces;
using PlacowkaOswiatowa.Domain.Resources;
using PlacowkaOswiatowa.ViewModels.Abstract;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;
using System;

namespace PlacowkaOswiatowa.ViewModels
{
    public class WszystkieAdresyViewModel : ItemsCollectionViewModel<AdresDto>, ILoadable
    {
        #region Pola i komendy

        private readonly IMapper _mapper;

        protected override Type ItemToCreateType => typeof(NowyAdresViewModel);

        #endregion


        #region Konstruktor
        public WszystkieAdresyViewModel(IPlacowkaRepository repository, IMapper mapper)
            : base(BaseResources.WszystkieAdresy, repository)
        {
            _mapper = mapper;
        }
        #endregion

        #region Incjacja
        public async Task LoadAsync()
        {
            try
            {
                var adresyFormDb = await _repository.Adresy.GetAllAsync(
                    includeProperties: "Panstwo,Miejscowosc,Ulica");
                var listaAdresow = _mapper.Map<IEnumerable<AdresDto>>(adresyFormDb);

                List = new ObservableCollection<AdresDto>(listaAdresow);
            }
            catch (Exception)
            {
                MessageBox.Show("Nie udało się pobrać adresów.", "Błąd",
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
            List = new ObservableCollection<AdresDto>
                (
                    _mapper.Map<IEnumerable<AdresDto>>
                        (
                            _repository.Adresy.GetAll()
                        )
                );
        }
        #endregion
    }
}