using PlacowkaOswiatowa.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PlacowkaOswiatowa.Domain.Interfaces.RepositoryInterfaces
{
    public interface IUczenRepository : IBaseEntityRepository<Uczen, int>
    {
        Task<List<Uczen>> GetAllAsync();
        List<Uczen> GetAll();
        Task<Uczen> GetUczenByPesel(string pesel);
        Task<Uczen> GetByIdAsync(int id);
    }
}
