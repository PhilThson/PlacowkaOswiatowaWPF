using PlacowkaOswiatowa.Domain.Models;
using System.Threading.Tasks;

namespace PlacowkaOswiatowa.Domain.Interfaces.RepositoryInterfaces
{
    public interface IPracownikRepository : IBaseEntityRepository<Pracownik, int>
    {
        Task<bool> UserExists(Uzytkownik uzytkownik);
    }
}
