﻿using PlacowkaOswiatowa.Domain.Interfaces.RepositoryInterfaces;
using PlacowkaOswiatowa.Domain.Models;
using PlacowkaOswiatowa.Domain.Resources;
using PlacowkaOswiatowa.ViewModels.Abstract;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;
using System;
using PlacowkaOswiatowa.Domain.Interfaces.CommonInterfaces;

namespace PlacowkaOswiatowa.ViewModels
{
    public class WszystkieOcenyViewModel : ItemsCollectionViewModel<Ocena>, ILoadable
    {
        #region Pola, właściwości, komendy
        protected override Type ItemToCreateType => typeof(NowyPrzedmiotViewModel);
        #endregion


        #region Konstruktor
        public WszystkieOcenyViewModel(IPlacowkaRepository repository)
            : base(BaseResources.WszystkieOceny, repository)
        { }
        #endregion

        #region Incjacja
        public async Task LoadAsync()
        {
            try
            {
                var listaOcen = await _repository.Oceny.GetAllAsync();

                List = new ObservableCollection<Ocena>(listaOcen);
            }
            catch (Exception)
            {
                MessageBox.Show("Nie udało się pobrać ocen.", "Błąd",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        #endregion

        #region Metody
        public override void Update()
        {
            Load();
        }

        public override void Load()
        {
            List = new ObservableCollection<Ocena>
                (
                    _repository.Oceny.GetAll(
                        includeProperties: "Uczen,Pracownik,Przedmiot")
                );
        }
        #endregion
    }
}
