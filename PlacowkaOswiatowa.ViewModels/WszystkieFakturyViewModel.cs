using PlacowkaOswiatowa.Domain.Interfaces.RepositoryInterfaces;
using PlacowkaOswiatowa.Domain.Resources;
using PlacowkaOswiatowa.ViewModels.Abstract;

namespace PlacowkaOswiatowa.ViewModels
{
    public class WszystkieFakturyViewModel : WorkspaceViewModel
    {
        #region Konstruktor
        public WszystkieFakturyViewModel(IPlacowkaRepository repository)
             : base(repository)
        {
            //base.DisplayName = "Faktury";
            base.DisplayName = BaseResources.Faktury;
        }
        #endregion
    }
}
