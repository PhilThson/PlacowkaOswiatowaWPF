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
    public class WszystkiePrzedmiotyViewModel : ItemsCollectionViewModel<PrzedmiotDto>, ILoadable
    {
        #region Konstruktor
        public WszystkiePrzedmiotyViewModel(IServiceProvider serviceProvider, IMapper mapper)
            : base(serviceProvider, mapper, BaseResources.WszystkiePrzedmioty)
        {
        }
        #endregion

        #region Incjacja
        public async Task LoadAsync()
        {
            var przedmioty = new List<Przedmiot>();
            using (var scope = _serviceProvider.CreateScope())
            {
                var repository = scope.ServiceProvider.GetRequiredService<IPlacowkaRepository>();
                przedmioty = await repository.Przedmioty.GetAllAsync();
            }
            AllList = _mapper.Map<List<PrzedmiotDto>>(przedmioty);
            List = new ObservableCollection<PrzedmiotDto>(AllList);
        }
        #endregion

        #region Metody
        protected override void Update() => Load();
        protected override void Load()
        {
            try
            {
                var przedmioty = new List<Przedmiot>();
                using (var scope = _serviceProvider.CreateScope())
                {
                    var repository = scope.ServiceProvider.GetRequiredService<IPlacowkaRepository>();
                    przedmioty = repository.Przedmioty.GetAll().ToList();
                }
                AllList = _mapper.Map<List<PrzedmiotDto>>(przedmioty);
                List = new ObservableCollection<PrzedmiotDto>(AllList);
            }
            catch (Exception e)
            {
                MessageBox.Show($"Nie udało się pobrać przedmiotów. '{e.Message}'", "Błąd",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        #endregion
    }
}
