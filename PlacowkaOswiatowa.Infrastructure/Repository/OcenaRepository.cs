using Microsoft.EntityFrameworkCore;
using PlacowkaOswiatowa.Domain.Interfaces.RepositoryInterfaces;
using PlacowkaOswiatowa.Domain.Models;
using PlacowkaOswiatowa.Infrastructure.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace PlacowkaOswiatowa.Infrastructure.Repository
{
    public class OcenaRepository : BaseEntityRepository<Ocena, long>, IOcenaRepository
    {
        private readonly DbSet<Ocena> _ocenaDbSet;

        public OcenaRepository(AplikacjaDbContext context) : base(context)
        {
            _ocenaDbSet = context.Set<Ocena>();
        }

        public override async Task<List<Ocena>> GetAllAsync(Expression<Func<Ocena, bool>> filter = null, string includeProperties = null)
        {
            return await _ocenaDbSet
                .AsNoTracking()
                .Include(o => o.Uczen)
                .Include(o => o.Pracownik)
                .Include(o => o.Przedmiot)
                .OrderBy(o => o.UczenId)
                .ToListAsync();
        }
    }
}
