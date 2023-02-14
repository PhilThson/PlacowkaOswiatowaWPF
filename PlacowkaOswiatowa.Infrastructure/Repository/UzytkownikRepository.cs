using PlacowkaOswiatowa.Domain.Interfaces.RepositoryInterfaces;
using PlacowkaOswiatowa.Domain.Models;
using PlacowkaOswiatowa.Infrastructure.DataAccess;

namespace PlacowkaOswiatowa.Infrastructure.Repository
{
    public class UzytkownikRepository : BaseEntityRepository<Uzytkownik, int>, 
        IUzytkownikRepository
    {
        public UzytkownikRepository(AplikacjaDbContext dbContext) : base(dbContext)
        {

        }
    }
}
