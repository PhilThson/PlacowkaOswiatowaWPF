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
    public class UrlopRepository : IUrlopRepository
    {
        private readonly DbSet<Urlop> _urlopDbSet;
        public UrlopRepository(AplikacjaDbContext dbContext)
        {
            _urlopDbSet = dbContext.Set<Urlop>();
        }

        public virtual bool Exists(int idPracownika, DateTime poczatekUrlopu)
        {
            return _urlopDbSet.Any(e => e.PracownikId == idPracownika && e.PoczatekUrlopu == poczatekUrlopu);
        }

        public IEnumerable<Urlop> GetAll()
        {
            return _urlopDbSet.Include(u => u.Pracownik).ToList();
        }

        public async Task<IEnumerable<Urlop>> GetAllAsync()
        {
            return await _urlopDbSet.Include(u => u.Pracownik).ToListAsync();
        }
    }
}
