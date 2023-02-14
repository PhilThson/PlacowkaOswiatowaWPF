using System.Threading.Tasks;

namespace PlacowkaOswiatowa.Domain.Interfaces.CommonInterfaces
{   
    /// <summary>
    /// Interfejs zapewnia możliwość asynchronicznego pobierania zasobów z bazy danych
    /// </summary>
    public interface ILoadable
    {
        Task LoadAsync();
    }
}
