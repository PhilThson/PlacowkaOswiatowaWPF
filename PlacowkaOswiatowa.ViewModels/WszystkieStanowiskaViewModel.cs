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
using Microsoft.Extensions.DependencyInjection;
using PlacowkaOswiatowa.Domain.Models;
using System.Linq;

namespace PlacowkaOswiatowa.ViewModels
{
    public class WszystkieStanowiskaViewModel : ItemsCollectionViewModel<StanowiskoDto>, ILoadable
    {

        #region Pola i komendy
        protected override Type ItemToCreateType => typeof(NoweStanowiskoViewModel);
        #endregion

        #region Konstruktor
        public WszystkieStanowiskaViewModel(IServiceProvider serviceProvider, IMapper mapper)
            : base(serviceProvider, mapper, BaseResources.WszystkiePrzedmioty)
        { 
        }
        #endregion

        #region Incjacja
        public async Task LoadAsync()
        {
            try
            {
                var stanowiska = new List<Stanowisko>();
                using (var scope = _serviceProvider.CreateScope())
                {
                    var repository = scope.ServiceProvider.GetRequiredService<IPlacowkaRepository>();
                    stanowiska = await repository.Stanowiska.GetAllAsync();
                }
                AllList = _mapper.Map<List<StanowiskoDto>>(stanowiska);
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

        protected override void Load()
        {
            var stanowiska = new List<Stanowisko>();
            using (var scope = _serviceProvider.CreateScope())
            {
                var repository = scope.ServiceProvider.GetRequiredService<IPlacowkaRepository>();
                stanowiska = repository.Stanowiska.GetAll().ToList();
            }
            AllList = _mapper.Map<List<StanowiskoDto>>(stanowiska);
            List = new ObservableCollection<StanowiskoDto>(AllList);
        }
        #endregion
    }
}
