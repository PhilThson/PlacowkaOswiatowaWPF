using System.Threading.Tasks;

namespace PlacowkaOswiatowa.Domain.Interfaces.CommonInterfaces
{
    /// <summary>
    /// Klasa implemetująca interfejs umożliwia edycję obiektów
    /// </summary>
    public interface IEditable
    {
        Task LoadItem(object objId);
    }
}
