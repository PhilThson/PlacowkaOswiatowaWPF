using PlacowkaOswiatowa.Domain.Interfaces.RepositoryInterfaces;
using PlacowkaOswiatowa.Domain.Models;
using PlacowkaOswiatowa.Infrastructure.DataAccess;

namespace PlacowkaOswiatowa.Infrastructure.Repository
{
    public class EtatRepository : BaseEntityRepository<Etat, byte>, IEtatRepository
    {
        public EtatRepository(AplikacjaDbContext context) : base(context)
        {

        }
    }
}
