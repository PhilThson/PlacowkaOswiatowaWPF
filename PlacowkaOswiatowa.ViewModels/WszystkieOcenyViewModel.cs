using PlacowkaOswiatowa.Domain.Interfaces.RepositoryInterfaces;
using PlacowkaOswiatowa.Domain.Resources;
using PlacowkaOswiatowa.ViewModels.Abstract;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;
using System;
using PlacowkaOswiatowa.Domain.Interfaces.CommonInterfaces;
using AutoMapper;
using PlacowkaOswiatowa.Domain.DTOs;
using System.Collections.Generic;

namespace PlacowkaOswiatowa.ViewModels
{
    public class WszystkieOcenyViewModel : ItemsCollectionViewModel<OcenaDto>, ILoadable
    {
        #region Pola, właściwości, komendy
        protected override Type ItemToCreateType => typeof(NowyPrzedmiotViewModel);
        #endregion


        #region Konstruktor
        public WszystkieOcenyViewModel(IPlacowkaRepository repository, IMapper mapper)
            : base(repository, mapper, BaseResources.WszystkieOceny)
        { }
        #endregion

        #region Incjacja
        public async Task LoadAsync()
        {
            try
            {
                var ocenyFromDb = await _repository.Oceny.GetAllAsync(
                    includeProperties: "Uczen,Pracownik,Przedmiot");
                AllList = _mapper.Map<List<OcenaDto>>(ocenyFromDb);
                List = new ObservableCollection<OcenaDto>(AllList);
            }
            catch (Exception)
            {
                MessageBox.Show("Nie udało się pobrać ocen.", "Błąd",
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
            AllList = _mapper.Map<List<OcenaDto>>(
                    _repository.Oceny.GetAll(
                        includeProperties: "Uczen,Pracownik,Przedmiot"));
            List = new ObservableCollection<OcenaDto>(AllList);
        }
        #endregion
    }
}
