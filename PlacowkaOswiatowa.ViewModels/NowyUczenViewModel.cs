﻿using PlacowkaOswiatowa.Domain.Interfaces.RepositoryInterfaces;
using PlacowkaOswiatowa.Domain.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using AutoMapper;
using PlacowkaOswiatowa.Domain.Interfaces.CommonInterfaces;
using PlacowkaOswiatowa.Domain.Commands;
using PlacowkaOswiatowa.Domain.Resources;
using PlacowkaOswiatowa.ViewModels.Abstract;
using PlacowkaOswiatowa.Domain.DTOs;
using PlacowkaOswiatowa.Domain.Exceptions;
using PlacowkaOswiatowa.Domain.Models.Base;
using System.Reflection;
using System.Linq;

namespace PlacowkaOswiatowa.ViewModels
{
    public class NowyUczenViewModel : SingleItemViewModel<UczenDto>, ILoadable
    {
        #region Pola i własności Osoby
        public string Imie
        {
            get => Item.Imie;
            set
            {
                if (value != Item.Imie)
                {
                    Item.Imie = value;

                    base.ClearErrors(nameof(Imie));
                    if (Item.Imie.Length < 3)
                        base.AddError(nameof(Imie), "Imię musi posiadać przynajmniej 3 znaki");

                    OnPropertyChanged(() => Imie);
                }
            }
        }
        public string DrugieImie
        {
            get => Item.DrugieImie;
            set 
            {
                if (value != Item.DrugieImie)
                {
                    Item.DrugieImie = value;

                    base.ClearErrors(nameof(DrugieImie));
                    if (Item.DrugieImie.Length != 0 && Item.DrugieImie.Length < 3)
                        base.AddError(nameof(DrugieImie), "Drugie imię musi posiadać przynajmniej 3 znaki");

                    OnPropertyChanged(() => DrugieImie);
                }
            }
        }
        public string Nazwisko
        {
            get => Item.Nazwisko;
            set
            {
                if (value != Item.Nazwisko)
                {
                    Item.Nazwisko = value;

                    //_errorsViewModel.ClearErrors(nameof(Nazwisko));
                    base.ClearErrors(nameof(Nazwisko));
                    if (Item.Nazwisko.Length < 3)
                        //_errorsViewModel.AddError(nameof(Nazwisko), "Nazwisko musi posiadać przynajmniej 3 znaki");
                        base.AddError(nameof(Nazwisko), "Nazwisko musi posiadać przynajmniej 3 znaki");

                    OnPropertyChanged(() => Nazwisko);
                }
            }
        }
        public string Pesel
        {
            get => Item.Pesel;
            set
            {
                if (value != Item.Pesel)
                {
                    Item.Pesel = value;
                    OnPropertyChanged(() => Pesel);
                }
            }
        }
        public DateTime? DataUrodzenia
        {
            get => Item.DataUrodzenia;
            set
            {
                if (value != Item.DataUrodzenia)
                {
                    Item.DataUrodzenia = value;
                    //_errorsViewModel.ClearErrors(nameof(DataUrodzenia));
                    base.ClearErrors(nameof(DataUrodzenia));
                    if (Item.DataUrodzenia > DateTime.Today)
                        //_errorsViewModel.AddError(nameof(DataUrodzenia),
                        base.AddError(nameof(DataUrodzenia),
                            "Nieprawidłowa data urodzenia");

                    OnPropertyChanged(() => DataUrodzenia);
                }
            }
        }
        #endregion

        #region Pola i właściwości Ucznia

        private ReadOnlyCollection<OddzialDto> _oddzialy;
        public ReadOnlyCollection<OddzialDto> Oddzialy
        {
            get => _oddzialy;
        }

        public OddzialDto WybranyOddzial
        {
            get => Item.Oddzial;
            set
            {
                if(value != null && value != Item.Oddzial)
                {
                    Item.Oddzial = value;
                    OnPropertyChanged(() => WybranyOddzial);
                }
            }
        }

        #endregion

        #region Pola i własności Adresu
        public string Panstwo
        {
            get => Item.Adres.Panstwo;
            set 
            { 
                if(value != Item.Adres.Panstwo)
                {
                    Item.Adres.Panstwo = value;
                    OnPropertyChanged(() => Panstwo);
                }
            }
        }
        public string Miejscowosc
        {
            get => Item.Adres.Miejscowosc;
            set 
            { 
                if(value != Item.Adres.Miejscowosc)
                {
                    Item.Adres.Miejscowosc = value;
                    OnPropertyChanged(() => Miejscowosc);
                }
            }
        }
        public string Ulica
        {
            get => Item.Adres.Ulica;
            set
            {
                if (value != Item.Adres.Ulica)
                {
                    Item.Adres.Ulica = value;
                    OnPropertyChanged(() => Ulica);
                }
            }
        }
        public string NumerDomu
        {
            get => Item.Adres.NumerDomu;
            set
            {
                if (value != Item.Adres.NumerDomu)
                {
                    Item.Adres.NumerDomu = value;
                    OnPropertyChanged(() => NumerDomu);
                }
            }
        }
        public string NumerMieszkania
        {
            get => Item.Adres.NumerMieszkania;
            set
            {
                if (value != Item.Adres.NumerMieszkania)
                {
                    Item.Adres.NumerMieszkania = value;
                    OnPropertyChanged(() => NumerMieszkania);
                }
            }
        }
        public string KodPocztowy
        {
            get => Item.Adres.KodPocztowy;
            set
            {
                if (value != Item.Adres.KodPocztowy)
                {
                    Item.Adres.KodPocztowy = value;
                    OnPropertyChanged(() => KodPocztowy);
                }
            }
        }
        #endregion

        #region Komendy

        public ICommand WyczyscFormularzCommand 
        { 
            get => new BaseCommand(WyczyscFormularz);
        }
        #endregion

        public bool CanSave => !base.HasErrors;

        #region Konstruktor
        public NowyUczenViewModel(IPlacowkaRepository repository, IMapper mapper)
            : base(BaseResources.NowyUczen, repository, mapper)
        {
            Item = new UczenDto { Adres = new AdresDto() };
            //Wyłączenie przycisku dopóki formularz nie przejdzie walidacji
            this.ErrorsChanged += (s, e) =>
                _SaveAndCloseCommand.RaiseCanExecuteChanged();
        }
        #endregion

        #region Pobieranie danych z bazy

        public async Task LoadAsync()
        {
            try
            {
                var grupyFromDb = await _repository.Oddzialy.GetAllAsync(includeProperties: "Pracownik");
                var listaOddzialow = _mapper.Map<List<OddzialDto>>(grupyFromDb);

                _oddzialy = new ReadOnlyCollection<OddzialDto>(listaOddzialow);
                OnPropertyChanged(() => Oddzialy);
            }
            catch (Exception e)
            {
                MessageBox.Show("Nie udało się załadować oddziałów.", "Error",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        #endregion

        #region Metody

        protected override async Task SaveAsync()
        {
            try
            {
                var uczen = _mapper.Map<Uczen>(Item);
                var adres = _mapper.Map<Adres>(Item.Adres);
                uczen.OddzialId = WybranyOddzial.Id;

                var adresFromDb = await _repository.Adresy.GetAdresAsync(adres);
                if (adresFromDb is not null)
                    uczen.AdresId = adresFromDb.Id;
                else
                {
                    uczen.Adres = adres;
                    //Dla powiązanych encji sprawdź czy istnieje rekord o zadanej nazwie,
                    //jeżeli tak, to pobierz istniejący, jeżeli nie, to utwórz nowy
                    var properties = uczen.Adres.GetType().GetProperties()
                        .Where(p => p.PropertyType.BaseType == typeof(BaseDictionaryEntity<int>))
                        .ToList();

                    foreach (var prop in properties)
                    {
                        var toSearch = this.GetType().GetProperty(prop.Name).GetValue(this);
                        MethodInfo method = _repository.GetType().GetMethod("GetByName",
                            BindingFlags.Public | BindingFlags.Instance);
                        MethodInfo genericMethod = method.MakeGenericMethod(prop.PropertyType);
                        var result = genericMethod.Invoke(_repository, new object[] { toSearch });
                        var entity = result as BaseDictionaryEntity<int>;
                        if (entity != null)
                        {
                            //Usuń wcześniej zmapowaną wartość
                            prop.SetValue(uczen.Adres, null);
                            //To wyrzuca, bo już automaper utworzył powiązane klasy
                            var propId = $"{prop.Name}Id";
                            var propIdInfo = uczen.Adres.GetType().GetProperty(propId);
                            propIdInfo.SetValue(uczen.Adres, entity.Id);
                        }
                    }
                }

                var czyIstnieje = await _repository.Uczniowie.Exists(uczen);

                if(!czyIstnieje)
                {
                    await _repository.Uczniowie.AddAsync(uczen);
                    await _repository.SaveAsync();

                    MessageBox.Show("Dodano ucznia!", "Sukces",
                        MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show("Uczeń już istnieje.", "Uwaga",
                        MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Nie udało się dodać ucznia", "Błąd",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
            //może rozgłaszać dodanie ucznia,
            //bo może być otwarta zakładka z listą pracowników,
            //która się nie odświeży
        }

        public void WyczyscFormularz()
        {
            Item = new UczenDto { Adres = new AdresDto() };
            base.ClearAllErrors();
            foreach(var prop in this.GetType().GetProperties())
                this.OnPropertyChanged(prop.Name);
        }

        protected override bool SaveAndCloseCanExecute() => !HasErrors;

        #endregion

        public override void Dispose()
        {
            //potrzebne w razie subskrybowania eventu z innej klasy
            //której czas życia trwa przez cały okres trwania aplikacji
            //potencjalne wycieki pamięci
            //this.PropertyChanged -= NowyUczenViewModel_PropertyChanged;
            base.Dispose();
        }
    }
}
