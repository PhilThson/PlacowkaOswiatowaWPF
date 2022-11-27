using Microsoft.EntityFrameworkCore;
using PlacowkaOswiatowa.Domain.Interfaces.RepositoryInterfaces;
using PlacowkaOswiatowa.Domain.Models;
using PlacowkaOswiatowa.Infrastructure.DataAccess;
using PlacowkaOswiatowa.Infrastructure.Extensions;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PlacowkaOswiatowa.Infrastructure.Repository
{
    public class PracownikRepository : BaseEntityRepository<Pracownik, int>, IPracownikRepository
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

        public async Task<IEnumerable<Pracownik>> GetAllAsync() =>
            await _pracownikDbSet
                .IncludeUmowa(p => p.PracownikUmowa)
                .Include(p => p.PracownikPrzedmiotyPracownicy)
                    .ThenInclude(pp => pp.Przedmiot)
                .IncludePracownicyAdresy()
                .ToListAsync();
    }
}
