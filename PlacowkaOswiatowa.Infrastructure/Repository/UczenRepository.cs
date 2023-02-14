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

        public async Task<List<Uczen>> GetAllAsync() =>
            await _uczenDbSet
            .AsNoTracking()
            .IncludeAdres(u => u.Adres)
            .Include(u => u.Oddzial)
                .ThenInclude(o => o.Pracownik)
            .ToListAsync();

        public List<Uczen> GetAll() =>
            _uczenDbSet
            .AsNoTracking()
            .IncludeAdres(u => u.Adres)
            .Include(u => u.Oddzial)
                .ThenInclude(o => o.Pracownik)
            .ToList();

        public async Task<Uczen> GetUczenByPesel(string pesel) =>
            await _uczenDbSet
            .AsNoTracking()
            .FirstOrDefaultAsync(u => u.Pesel == pesel);

        public async Task<Uczen> GetByIdAsync(int id) =>
            await _uczenDbSet
            .AsNoTracking()
            .Where(u => u.Id == id)
            .IncludeAdres(u => u.Adres)
            .Include(u => u.Oddzial)
                .ThenInclude(o => o.Pracownik)
            .FirstOrDefaultAsync();

    }
}
