using PlacowkaOswiatowa.Domain.Interfaces.RepositoryInterfaces;
using PlacowkaOswiatowa.Domain.Resources;
using PlacowkaOswiatowa.ViewModels.Abstract;

namespace PlacowkaOswiatowa.ViewModels
{
    public class NowyTowarViewModel : WorkspaceViewModel
    {
        #region Konstruktor
        public NowyTowarViewModel(IPlacowkaRepository repository)
            : base(repository)
        {
            base.DisplayName = BaseResources.Towar;
        }
        #endregion
    }
}
