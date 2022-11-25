using PlacowkaOswiatowa.Domain.Interfaces.RepositoryInterfaces;
using PlacowkaOswiatowa.Domain.Models;
using PlacowkaOswiatowa.Infrastructure.DataAccess;

namespace PlacowkaOswiatowa.Infrastructure.Repository
{
    public class StanowiskoRepository : BaseEntityRepository<Stanowisko, byte>, IStanowiskoRepository
    {
        public StanowiskoRepository(AplikacjaDbContext context) : base(context)
        {

        }
    }
}
