using Microsoft.EntityFrameworkCore;
using PlacowkaOswiatowa.Domain.Interfaces.RepositoryInterfaces;
using PlacowkaOswiatowa.Domain.Models;
using PlacowkaOswiatowa.Infrastructure.DataAccess;
using PlacowkaOswiatowa.Infrastructure.Extensions;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PlacowkaOswiatowa.Infrastructure.Repository
{
    public class PracodawcaRepository : BaseEntityRepository<Pracodawca, byte>, 
        IPracodawcaRepository
    {
        private readonly DbSet<Pracodawca> _pracodawcaSet;

        public PracodawcaRepository(AplikacjaDbContext context) : base(context)
        {
            _pracodawcaSet = context.Set<Pracodawca>();
        }

        public async Task<List<Pracodawca>> GetAllAsync() =>
            await _pracodawcaSet
                .IncludeAdres(p => p.Adres)
                .ToListAsync();
    }
}
