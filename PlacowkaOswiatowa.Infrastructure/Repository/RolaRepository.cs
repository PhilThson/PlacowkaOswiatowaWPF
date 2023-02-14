using PlacowkaOswiatowa.Domain.Interfaces.RepositoryInterfaces;
using PlacowkaOswiatowa.Domain.Models;
using PlacowkaOswiatowa.Infrastructure.DataAccess;

namespace PlacowkaOswiatowa.Infrastructure.Repository
{
    public class RolaRepository : BaseEntityRepository<Rola, byte>, IRolaRepository
    {
        public RolaRepository(AplikacjaDbContext dbContext) : base(dbContext)
        {

        }
    }
}
