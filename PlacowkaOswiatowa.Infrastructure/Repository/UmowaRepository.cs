using Microsoft.EntityFrameworkCore;
using PlacowkaOswiatowa.Domain.DTOs;
using PlacowkaOswiatowa.Domain.Interfaces.RepositoryInterfaces;
using PlacowkaOswiatowa.Domain.Models;
using PlacowkaOswiatowa.Infrastructure.DataAccess;
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

        public async Task<bool> Exists(UmowaDto umowa)
        {
            return await _umowaDbSet.AnyAsync(u => u.PracodawcaId == umowa.Pracodawca.Id &&
                                                u.PracownikId == umowa.Pracownik.Id);
        }
    }
}
