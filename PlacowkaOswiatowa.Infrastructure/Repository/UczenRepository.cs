using PlacowkaOswiatowa.Domain.Interfaces.RepositoryInterfaces;
using PlacowkaOswiatowa.Domain.Models;
using PlacowkaOswiatowa.Infrastructure.DataAccess;

namespace PlacowkaOswiatowa.Infrastructure.Repository
{
    public class UczenRepository : BaseEntityRepository<Uczen, int>, IUczenRepository
    {
        public UczenRepository(AplikacjaDbContext dbContext) : base(dbContext)
        {

        }
    }
}
