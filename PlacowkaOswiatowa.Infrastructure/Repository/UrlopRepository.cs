using Microsoft.EntityFrameworkCore;
using PlacowkaOswiatowa.Domain.Interfaces.RepositoryInterfaces;
using PlacowkaOswiatowa.Domain.Models;
using PlacowkaOswiatowa.Infrastructure.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlacowkaOswiatowa.Infrastructure.Repository
{
    //repozytorium encji z kluczem złożonym, dlatego nie dziedziczy po BaseRepository
    public class UrlopRepository : IUrlopRepository
    {
        private readonly DbSet<Urlop> _urlopDbSet;
        public UrlopRepository(AplikacjaDbContext dbContext)
        {
            _urlopDbSet = dbContext.Set<Urlop>();
        }

        public bool Exists(int idPracownika, DateTime poczatekUrlopu, DateTime koniecUrlopu)
        {
            return _urlopDbSet.Any(u => u.PracownikId == idPracownika &&
                                    (u.PoczatekUrlopu < koniecUrlopu && 
                                    poczatekUrlopu < u.KoniecUrlopu));
        }

        public List<Urlop> GetAll()
        {
            return _urlopDbSet
                .AsNoTracking()
                .Include(u => u.Pracownik)
                .ToList();
        }

        public async Task<List<Urlop>> GetAllAsync()
        {
            return await _urlopDbSet
                .AsNoTracking()
                .Include(u => u.Pracownik).ToListAsync();
        }

        public async Task AddAsync(Urlop urlop)
        {
            await _urlopDbSet.AddAsync(urlop);
        }
    }
}
