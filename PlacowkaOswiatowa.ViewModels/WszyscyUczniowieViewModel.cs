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
    public class WszyscyUczniowieViewModel : ItemsCollectionViewModel<UczenDto>, ILoadable
    {
        #region Pola, właściwości, komendy
        private readonly IMapper _mapper;
        protected override Type ItemToCreateType => typeof(NowyUczenViewModel);
        #endregion

        #region Konstruktor
        public WszyscyUczniowieViewModel(IPlacowkaRepository repository, IMapper mapper)
            : base(BaseResources.WszyscyUczniowie, repository)
        {
            _mapper = mapper;   
        }
        #endregion

        #region Inicjacja
        //[Obsolete("Metoda statyczna do asynchronicznego ładowania ViewModelu")]
        //public async static Task<WszyscyUczniowieViewModel> Load(IPlacowkaRepository repository, IMapper mapper)
        //{
        //    var viewModel = new WszyscyUczniowieViewModel(repository, mapper);
        //    Task.Run(async () => await viewModel.DownloadAsync());
        //    return viewModel;
        //}

        public async Task LoadAsync()
        {
            try
            {
                var uczniowieFromDb = await _repository.Uczniowie.GetAllAsync();
                var listaUczniow =
                    _mapper.Map<IEnumerable<UczenDto>>(uczniowieFromDb);

                List = new ObservableCollection<UczenDto>(listaUczniow);
                //OnPropertyChanged(() => List);
            }
            catch(Exception)
            {
                MessageBox.Show("Nie udało się pobrać listy uczniów.", "Błąd",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        #endregion

        #region Helpers
        //Ta metoda zostanie raczej podpięta pod przycisk 'Odswież'
        public override void Load()
        {
            List = new ObservableCollection<UczenDto>
                (
                    _mapper.Map<IEnumerable<UczenDto>>
                        (
                            _repository.Uczniowie.GetAll()
                        )
                );
        }

        public override void Update() => Load();
        #endregion
    }
}
