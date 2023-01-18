using Microsoft.EntityFrameworkCore;
using PlacowkaOswiatowa.Domain.Interfaces.RepositoryInterfaces;
using PlacowkaOswiatowa.Domain.Models;
using PlacowkaOswiatowa.Infrastructure.DataAccess;
using PlacowkaOswiatowa.Infrastructure.Extensions;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlacowkaOswiatowa.Infrastructure.Repository
{
    public class UczenRepository : BaseEntityRepository<Uczen, int>, IUczenRepository
    {
        private readonly DbSet<Uczen> _uczenDbSet;
        public UczenRepository(AplikacjaDbContext dbContext) : base(dbContext)
        {
            _uczenDbSet = dbContext.Set<Uczen>();
        }

        public async Task<IEnumerable<Uczen>> GetAllAsync() =>
            await _uczenDbSet
            .IncludeAdres(u => u.Adres)
            .Include(u => u.Oddzial)
                .ThenInclude(o => o.Pracownik)
            .ToListAsync();

        public IEnumerable<Uczen> GetAll() =>
            _uczenDbSet
            .IncludeAdres(u => u.Adres)
            .Include(u => u.Oddzial)
                .ThenInclude(o => o.Pracownik)
            .ToList();

        public async Task<Uczen> GetUczenByPesel(string pesel) =>
            await _uczenDbSet.FirstOrDefaultAsync(u => u.Pesel == pesel);
    }
}
