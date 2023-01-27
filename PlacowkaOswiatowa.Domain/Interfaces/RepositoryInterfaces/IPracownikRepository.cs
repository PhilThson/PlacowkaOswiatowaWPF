using PlacowkaOswiatowa.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PlacowkaOswiatowa.Domain.Interfaces.RepositoryInterfaces
{
    public interface IPracownikRepository : IBaseEntityRepository<Pracownik, int>
    {
        Task<bool> UserExists(Uzytkownik uzytkownik);
        Task<List<Pracownik>> GetAllAsync();
        List<Pracownik> GetAll();
        Task<Pracownik> GetPracownikByPeselAsync(string pesel);
        Task<Pracownik> GetByIdAsync(int id);
    }
}
