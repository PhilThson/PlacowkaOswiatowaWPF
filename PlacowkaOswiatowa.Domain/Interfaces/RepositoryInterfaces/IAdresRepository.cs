using PlacowkaOswiatowa.Domain.Models;
using System.Threading.Tasks;

namespace PlacowkaOswiatowa.Domain.Interfaces.RepositoryInterfaces
{
    public interface IAdresRepository : IBaseEntityRepository<Adres, int>
    {
        Task<Adres> GetAdresAsync(Adres adres);
    }
}
