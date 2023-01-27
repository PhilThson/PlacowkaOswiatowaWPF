using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using PlacowkaOswiatowa.Domain.DTOs;
using PlacowkaOswiatowa.Domain.Interfaces.CommonInterfaces;
using PlacowkaOswiatowa.Domain.Interfaces.RepositoryInterfaces;
using PlacowkaOswiatowa.Domain.Models;
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
        public WszystkieUrlopyViewModel(IServiceProvider serviceProvider, IMapper mapper)
            : base(serviceProvider, mapper, BaseResources.WszystkieUrlopy)
        { 
        }
        #endregion

        #region Incjacja
        public async Task LoadAsync()
        {
            try
            {
                var urlopy = new List<Urlop>();
                using (var scope = _serviceProvider.CreateScope())
                {
                    var repository = scope.ServiceProvider.GetRequiredService<IPlacowkaRepository>();
                    urlopy = await repository.Urlopy.GetAllAsync();
                }
                AllList = _mapper.Map<List<UrlopPracownikaDto>>(urlopy);
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
            var urlopy = new List<Urlop>();
            using (var scope = _serviceProvider.CreateScope())
            {
                var repository = scope.ServiceProvider.GetRequiredService<IPlacowkaRepository>();
                urlopy = repository.Urlopy.GetAll();
            }
            AllList = _mapper.Map<List<UrlopPracownikaDto>>(urlopy);
            List = new ObservableCollection<UrlopPracownikaDto>(AllList);
        }
        #endregion
    }
}
