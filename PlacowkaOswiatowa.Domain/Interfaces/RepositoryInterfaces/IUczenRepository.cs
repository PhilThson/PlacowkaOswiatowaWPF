using PlacowkaOswiatowa.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PlacowkaOswiatowa.Domain.Interfaces.RepositoryInterfaces
{
    public interface IUczenRepository : IBaseEntityRepository<Uczen, int>
    {
        Task<IEnumerable<Uczen>> GetAllAsync();
        IEnumerable<Uczen> GetAll();
        Task<Uczen> GetUczenByPesel(string pesel);
    }
}
