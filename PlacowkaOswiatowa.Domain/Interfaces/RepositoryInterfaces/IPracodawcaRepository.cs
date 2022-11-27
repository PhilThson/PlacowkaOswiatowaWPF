using PlacowkaOswiatowa.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PlacowkaOswiatowa.Domain.Interfaces.RepositoryInterfaces
{
    public interface IPracodawcaRepository : IBaseEntityRepository<Pracodawca, byte>
    {
        Task<IEnumerable<Pracodawca>> GetAllAsync();
    }
}
