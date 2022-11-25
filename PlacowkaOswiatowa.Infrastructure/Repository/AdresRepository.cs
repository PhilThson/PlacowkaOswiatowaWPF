using Microsoft.EntityFrameworkCore;
using PlacowkaOswiatowa.Domain.Interfaces.RepositoryInterfaces;
using PlacowkaOswiatowa.Domain.Models;
using PlacowkaOswiatowa.Infrastructure.DataAccess;
using System.Threading.Tasks;

namespace PlacowkaOswiatowa.Infrastructure.Repository
{
    public class AdresRepository : BaseEntityRepository<Adres, int>, IAdresRepository
    {
        private readonly DbSet<Adres> _adresDbSet;

        public AdresRepository(AplikacjaDbContext dbContext) : base(dbContext)
        {
            _adresDbSet = dbContext.Set<Adres>();
        }

        public async Task<bool> Exists(int id)
        {
            return await _adresDbSet.AnyAsync(a => a.Id == id);
        }
        public async Task<bool> Exists(Adres adres)
        {
            return await _adresDbSet.AnyAsync(a => a.Ulica == adres.Ulica &&
                a.Miejscowosc == adres.Miejscowosc &&
                a.NumerDomu == adres.NumerDomu &&
                a.NumerMieszkania == adres.NumerMieszkania &&
                a.KodPocztowy == adres.KodPocztowy);
        }
    }
}
