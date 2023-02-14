using Microsoft.EntityFrameworkCore;
using PlacowkaOswiatowa.Domain.Interfaces.RepositoryInterfaces;
using PlacowkaOswiatowa.Domain.Models;
using PlacowkaOswiatowa.Infrastructure.DataAccess;
using System.Threading.Tasks;

namespace PlacowkaOswiatowa.Infrastructure.Repository
{
    public class OddzialRepository : BaseEntityRepository<Oddzial, byte>, IOddzialRepository
    {
        private readonly DbSet<Oddzial> _oddzialDbSet;
        public OddzialRepository(AplikacjaDbContext dbContext) : base(dbContext)
        {
            _oddzialDbSet = dbContext.Set<Oddzial>();
        }
        public async Task<bool> Exists(int id)
        {
            return await _oddzialDbSet.AnyAsync(e => e.Id == id);
        }
    }
}
