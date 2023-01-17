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
    public class WszyscyPracownicyViewModel : ItemsCollectionViewModel<PracownikDto>, ILoadable
    {
        #region Pola i komendy
        protected override Type ItemToCreateType => typeof(NowyPracownikViewModel);
        #endregion

        #region Konstruktor
        public WszyscyPracownicyViewModel(IPlacowkaRepository repository, IMapper mapper)
            : base(repository, mapper, BaseResources.WszyscyPracownicy)
        {
        }
        #endregion

        #region Incjacja
        public async Task LoadAsync()
        {
            try
            {
                var pracownicyFormDb = await _repository.Pracownicy.GetAllAsync();
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

        #region Metody
        protected override void Update()
        {
            Load();
        }

        protected override void Load()
        {
            List = new ObservableCollection<PracownikDto>
                (
                    _mapper.Map<IEnumerable<PracownikDto>>
                        (
                            _repository.Pracownicy.GetAllAsync()
                        )
                );
        }

        protected override void OrderBy()
        {
            throw new NotImplementedException();
        }

        protected override void Filter()
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
