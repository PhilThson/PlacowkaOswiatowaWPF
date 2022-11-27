using PlacowkaOswiatowa.Domain.DTOs;
using PlacowkaOswiatowa.Domain.Models;
using System.Threading.Tasks;

namespace PlacowkaOswiatowa.Domain.Interfaces.RepositoryInterfaces
{
    public interface IUmowaRepository : IBaseEntityRepository<Umowa, int>
    {
        Task<bool> Exists(UmowaDto umowa);
    }
}
