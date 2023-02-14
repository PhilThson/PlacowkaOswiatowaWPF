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
using Microsoft.Extensions.DependencyInjection;
using PlacowkaOswiatowa.Domain.Models;
using System.Linq;

namespace PlacowkaOswiatowa.ViewModels
{
    public class WszystkieOcenyViewModel : ItemsCollectionViewModel<OcenaDto>, ILoadable
    {
        #region Konstruktor
        public WszystkieOcenyViewModel(IServiceProvider serviceProvide, IMapper mapper)
            : base(serviceProvide, mapper, BaseResources.WszystkieOceny)
        { }
        #endregion

        #region Incjacja
        public async Task LoadAsync()
        {
            var oceny = new List<Ocena>();
            using (var scope = _serviceProvider.CreateScope())
            {
                var repository = scope.ServiceProvider.GetRequiredService<IPlacowkaRepository>();
                oceny = await repository.Oceny.GetAllAsync(
                    includeProperties: "Uczen,Pracownik,Przedmiot");
            }
            AllList = _mapper.Map<List<OcenaDto>>(oceny);
            List = new ObservableCollection<OcenaDto>(AllList);
        }
        #endregion

        #region Metody
        protected override void Update() => Load();

        protected override void Load()
        {
            try
            {
                var oceny = new List<Ocena>();
                using (var scope = _serviceProvider.CreateScope())
                {
                    var repository = scope.ServiceProvider.GetRequiredService<IPlacowkaRepository>();
                    oceny = repository.Oceny.GetAll(
                        includeProperties: "Uczen,Pracownik,Przedmiot").ToList();
                }
                AllList = _mapper.Map<List<OcenaDto>>(oceny);
                List = new ObservableCollection<OcenaDto>(AllList);
            }
            catch (Exception)
            {
                MessageBox.Show("Nie udało się pobrać ocen.", "Błąd",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        #endregion
    }
}
