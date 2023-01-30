using AutoMapper;
using PlacowkaOswiatowa.Domain.DTOs;
using PlacowkaOswiatowa.Domain.Models.Base;
using PlacowkaOswiatowa.Domain.Models;
using PlacowkaOswiatowa.Domain.Resources;
using PlacowkaOswiatowa.ViewModels.Abstract;
using System;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using PlacowkaOswiatowa.Domain.Interfaces.RepositoryInterfaces;
using System.Windows;
using PlacowkaOswiatowa.Domain.Exceptions;
using PlacowkaOswiatowa.Domain.Interfaces.CommonInterfaces;
using System.Collections.ObjectModel;
using System.Collections.Generic;

namespace PlacowkaOswiatowa.ViewModels
{
    public class NowyAdresViewModel : SingleItemViewModel<AdresDto>, 
        ILoadable, IEditable
    {
        #region Konstruktor
        public NowyAdresViewModel(IServiceProvider serviceProvider, IMapper mapper)
            : base(serviceProvider, mapper, BaseResources.NowyAdres)
        {
            Item = new AdresDto();
        }
        #endregion

        #region Własności Adresu
        public object Id 
        {
            get => Item.Id;
            set
            {
                if(value != Item.Id)
                {
                    Item.Id = value;
                }
            }
        }
        public string Panstwo
        {
            get => Item.Panstwo;
            set
            {
                if (value != Item.Panstwo)
                {
                    Item.Panstwo = value;
                    ClearErrors(nameof(Panstwo));
                    if (Item.Panstwo.Length < 1)
                        AddError(nameof(Panstwo), "Należy podać Państwo");
                    OnPropertyChanged();
                }
            }
        }
        public string Miejscowosc
        {
            get => Item.Miejscowosc;
            set
            {
                if (value != Item.Miejscowosc)
                {
                    Item.Miejscowosc = value;
                    ClearErrors(nameof(Miejscowosc));
                    if (Item.Miejscowosc.Length < 1)
                        AddError(nameof(Miejscowosc), "Należy podać miejscowość");
                    OnPropertyChanged();
                }
            }
        }
        public string Ulica
        {
            get => Item.Ulica;
            set
            {
                if (value != Item.Ulica)
                {
                    Item.Ulica = value;
                    OnPropertyChanged();
                }
            }
        }
        public string NumerDomu
        {
            get => Item.NumerDomu;
            set
            {
                if (value != Item.NumerDomu)
                {
                    Item.NumerDomu = value;
                    ClearErrors(nameof(NumerDomu));
                    if (Item.NumerDomu.Length < 1)
                        AddError(nameof(NumerDomu), "Należy podać numer domu");
                    OnPropertyChanged();
                }
            }
        }
        public string NumerMieszkania
        {
            get => Item.NumerMieszkania;
            set
            {
                if (value != Item.NumerMieszkania)
                {
                    Item.NumerMieszkania = value;
                    OnPropertyChanged();
                }
            }
        }
        public string KodPocztowy
        {
            get => Item.KodPocztowy;
            set
            {
                if (value != Item.KodPocztowy)
                {
                    Item.KodPocztowy = value;
                    ClearErrors(nameof(KodPocztowy));
                    if (Item.KodPocztowy.Length < 1)
                        AddError(nameof(KodPocztowy), "Należy podać kod pocztowy");
                    OnPropertyChanged();
                }
            }
        }
        #endregion

        #region Wybór adresu z listy
        private ObservableCollection<AdresDto> _adresy;
        public ObservableCollection<AdresDto> Adresy
        {
            get => _adresy;
            set => SetProperty(ref _adresy, value);
        }

        private AdresDto _WybranyAdres;
        public AdresDto WybranyAdres
        {
            get => _WybranyAdres;
            set
            {
                if (value != _WybranyAdres)
                {
                    _WybranyAdres = value;
                    ClearAllErrors();
                    OnPropertyChanged();
                }
            }
        }
        #endregion

        #region Metody
        protected override async Task<bool> SaveAsync()
        {
            try
            {
                if(WybranyAdres != null)
                {
                    //jeżeli został wskazany adres z listy, to jest on zwracany
                    //do metody wywołującej okno modalne
                    Item = WybranyAdres;
                    return true;
                }

                var adres = _mapper.Map<Adres>(Item);

                using (var scope = _serviceProvider.CreateScope())
                {
                    var repository = scope.ServiceProvider.GetRequiredService<IPlacowkaRepository>();

                    var adresById = await repository.Adresy.GetAsync(a => a.Id == adres.Id,
                    includeProperties: "Panstwo,Miejscowosc,Ulica");

                    if (adresById == adres)
                        throw new DataValidationException("Nie dokonano zmian");

                    var adresFromDb = await repository.Adresy.GetAdresAsync(adres);
                    if (adresFromDb is not null)
                        throw new DataValidationException(
                            "Adres o wskazanych parametrach już istnieje");

                    var properties = adres.GetType().GetProperties()
                        .Where(p => p.PropertyType.BaseType == typeof(BaseDictionaryEntity<int>))
                        .ToList();

                    foreach (var prop in properties)
                    {
                        var toSearch = this.GetType().GetProperty(prop.Name).GetValue(this);
                        MethodInfo method = repository.GetType().GetMethod("GetByName",
                            BindingFlags.Public | BindingFlags.Instance);
                        MethodInfo genericMethod = method.MakeGenericMethod(prop.PropertyType);
                        var result = genericMethod.Invoke(repository, new object[] { toSearch });
                        var entity = result as BaseDictionaryEntity<int>;
                        if (entity != null)
                        {
                            prop.SetValue(adres, null);
                            var propId = $"{prop.Name}Id";
                            var propIdInfo = adres.GetType().GetProperty(propId);
                            propIdInfo.SetValue(adres, entity.Id);
                        }
                    }

                    await repository.AddAsync(adres);
                    await repository.SaveAsync();
                }
                //Item zostanie odczytany przez zakładkę która wywołała widok adresu
                Item.Id = adres.Id;

                return true;
            }
            catch (DataValidationException e)
            {
                MessageBox.Show(e.Message, "Uwaga",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            catch (Exception e)
            {
                MessageBox.Show($"Nie udało się zapisać adresu. {e.Message}", "Error",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
            return false;
        }

        protected override void CheckRequiredProperties()
        {
            if (WybranyAdres != null)
                return;

            BaseCheckRequiredProperties(
                nameof(Panstwo),
                nameof(Miejscowosc),
                nameof(NumerDomu),
                nameof(KodPocztowy));
        }

        public async Task LoadItem(object objId)
        {
            var adresId = Convert.ToInt32(objId);
            if (adresId == default)
                throw new ArgumentException("Przesłano nieprawidłowy identyfikator obiektu");

            Adres adres = null;

            using (var scope = _serviceProvider.CreateScope())
            {
                var repository = scope.ServiceProvider.GetRequiredService<IPlacowkaRepository>();
                adres = await repository.Adresy.GetAsync(a => a.Id == adresId,
                    includeProperties:"Panstwo,Miejscowosc,Ulica");
            }

            _ = adres ?? throw new DataNotFoundException(
                $"Nie znaleziono adresu o podanym identyfikatorze {adresId}");

            base.DisplayName = BaseResources.EdycjaAdresu;
            base.AddItemName = BaseResources.SaveItem;

            Item = _mapper.Map<AdresDto>(adres);

            foreach (var prop in Item.GetType().GetProperties())
                OnPropertyChanged(prop.Name);
        }

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

                var listaAdresow = _mapper.Map<List<AdresDto>>(adresy);
                Adresy = new ObservableCollection<AdresDto>(listaAdresow);
            }
            catch (Exception e)
            {
                MessageBox.Show("Nie udało się załadować adresów.", "Error",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        protected override void ClearForm(object _)
        {
            Item = new AdresDto();
            WybranyAdres = null;
            ClearValidationMessages();
            ClearAllErrors();
            foreach (var prop in this.GetType().GetProperties())
                OnPropertyChanged(prop.Name);
        }

        #endregion

    }
}
