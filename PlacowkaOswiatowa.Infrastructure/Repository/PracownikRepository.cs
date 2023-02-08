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
    public class PracownikRepository : BaseEntityRepository<Pracownik, int>, 
        IPracownikRepository
    {
        private readonly DbSet<Pracownik> _pracownikDbSet;

        public PracownikRepository(AplikacjaDbContext dbContext) : base(dbContext)
        {
            _pracownikDbSet = dbContext.Set<Pracownik>();
        }

        public async Task<bool> UserExists(Uzytkownik uzytkownik)
        {
            return await _pracownikDbSet.AnyAsync(p => p.Imie == uzytkownik.Email &&
                                            p.Nazwisko == uzytkownik.HashHasla);
        }

        public async Task<List<Pracownik>> GetAllAsync() =>
            await _pracownikDbSet
                .AsNoTracking()
                .IncludeUmowa(p => p.PracownikUmowa)
                .IncludePracownicyAdresy()
                .ToListAsync();

        public List<Pracownik> GetAll() =>
            _pracownikDbSet
                .AsNoTracking()
                .IncludeUmowa(p => p.PracownikUmowa)
                .IncludePracownicyAdresy()
                .ToList();

        public async Task<Pracownik> GetPracownikByPeselAsync(string pesel) =>
            await _pracownikDbSet
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.Pesel == pesel);

        public async Task<Pracownik> GetByIdAsync(int id) =>
            await _pracownikDbSet
                .AsNoTracking()
                .Where(p => p.Id == id)
                .IncludeUmowa(p => p.PracownikUmowa)
                .IncludePracownicyAdresy()
                .FirstOrDefaultAsync();
    }
}
