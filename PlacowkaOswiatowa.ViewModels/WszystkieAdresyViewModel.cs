using AutoMapper;
using PlacowkaOswiatowa.Domain.DTOs;
using PlacowkaOswiatowa.Domain.Interfaces.CommonInterfaces;
using PlacowkaOswiatowa.Domain.Resources;
using PlacowkaOswiatowa.ViewModels.Abstract;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;
using System;
using Microsoft.Extensions.DependencyInjection;
using PlacowkaOswiatowa.Domain.Interfaces.RepositoryInterfaces;
using PlacowkaOswiatowa.Domain.Models;
using System.Linq;

namespace PlacowkaOswiatowa.ViewModels
{
    public class WszystkieAdresyViewModel : ItemsCollectionViewModel<AdresDto>, ILoadable
    {
        #region Pola i komendy
        protected override Type ItemToCreateType => typeof(NowyAdresViewModel);

        #endregion


        #region Konstruktor
        public WszystkieAdresyViewModel(IServiceProvider serviceProvider, IMapper mapper)
            : base(serviceProvider, mapper, BaseResources.WszystkieAdresy)
        {
        }
        #endregion

        #region Incjacja
        public async Task LoadAsync()
        {
            try
            {
                var adresy = new List<Adres>();
                using (var scope = _serviceProvider.CreateScope())
                {
                    var repository = scope.ServiceProvider.GetRequiredService<IPlacowkaRepository>();
                    adresy = await repository.Adresy.GetAllAsync(
                        includeProperties: "Panstwo,Miejscowosc,Ulica");
                }
                AllList = _mapper.Map<List<AdresDto>>(adresy);
                List = new ObservableCollection<AdresDto>(AllList);
            }
            catch (Exception)
            {
                MessageBox.Show("Nie udało się pobrać adresów.", "Błąd",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        #endregion

        #region Metody
        protected override void Update()
        {
            Load();
        }

        //Ta metoda zostanie prawdopodobnie podpięta pod przycisk 'Odswież'
        protected override void Load()
        {
            var adresy = new List<Adres>();
            using (var scope = _serviceProvider.CreateScope())
            {
                var repository = scope.ServiceProvider.GetRequiredService<IPlacowkaRepository>();
                adresy = repository.Adresy.GetAll(
                    includeProperties: "Panstwo,Miejscowosc,Ulica").ToList();
            }
            AllList = _mapper.Map<List<AdresDto>>(adresy);
            List = new ObservableCollection<AdresDto>(AllList);
        }
        #endregion
    }
}