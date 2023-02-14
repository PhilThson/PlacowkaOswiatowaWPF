using PlacowkaOswiatowa.Domain.Interfaces.RepositoryInterfaces;
using PlacowkaOswiatowa.Domain.Models;
using PlacowkaOswiatowa.Infrastructure.DataAccess;

namespace PlacowkaOswiatowa.Infrastructure.Repository
{
    public class PrzedmiotRepository : BaseEntityRepository<Przedmiot, byte>, IPrzedmiotRepository
    {
        public PrzedmiotRepository(AplikacjaDbContext context) : base(context)
        {

        }
    }
}
