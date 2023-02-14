using Microsoft.EntityFrameworkCore;
using PlacowkaOswiatowa.Domain.DTOs;
using PlacowkaOswiatowa.Domain.Interfaces.RepositoryInterfaces;
using PlacowkaOswiatowa.Domain.Models;
using PlacowkaOswiatowa.Infrastructure.DataAccess;
using PlacowkaOswiatowa.Infrastructure.Extensions;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlacowkaOswiatowa.Infrastructure.Repository
{
    public class UmowaRepository : BaseEntityRepository<Umowa, int>, IUmowaRepository
    {
        private readonly DbSet<Umowa> _umowaDbSet;
        public UmowaRepository(AplikacjaDbContext context) : base(context)
        {
            _umowaDbSet = context.Set<Umowa>();
        }

        public async Task<bool> Exists(UmowaDto umowa) =>
            await _umowaDbSet.AnyAsync(u => u.PracodawcaId == umowa.Pracodawca.Id &&
                                            u.PracownikId == (int)umowa.Pracownik.Id);

        public bool DoesEmployeeHasAgreement(int employeeId) =>
            _umowaDbSet.Any(u => u.PracownikId == employeeId);

        public async Task<List<Umowa>> GetAllAsync() =>
            await _umowaDbSet
            .AsNoTracking()
            .IncludePracodawca(u => u.Pracodawca)
            .Include(u => u.Pracownik)
            .Include(u => u.Etat)
            .Include(u => u.Stanowisko)
            .ToListAsync();

        public List<Umowa> GetAll() =>
            _umowaDbSet
            .AsNoTracking()
            .IncludePracodawca(u => u.Pracodawca)
            .Include(u => u.Pracownik)
            .Include(u => u.Etat)
            .Include(u => u.Stanowisko)
            .ToList();

        public async Task<Umowa> GetByIdAsync(int id) =>
            await _umowaDbSet
            .AsNoTracking()
            .Where(u => u.Id == id)
            .IncludePracodawca(u => u.Pracodawca)
            .Include(u => u.Pracownik)
            .Include(u => u.Etat)
            .Include(u => u.Stanowisko)
            .FirstOrDefaultAsync();

    }
}
