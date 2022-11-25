using PlacowkaOswiatowa.Domain.Interfaces.RepositoryInterfaces;
using PlacowkaOswiatowa.Domain.Resources;
using PlacowkaOswiatowa.ViewModels.Abstract;

namespace PlacowkaOswiatowa.ViewModels
{
    public class NowaFakturaViewModel : WorkspaceViewModel
    {
        #region Konstruktor
        public NowaFakturaViewModel(IPlacowkaRepository repository)
            : base(repository)
        {
            base.DisplayName = BaseResources.Faktura;
        }
        #endregion
    }
}
