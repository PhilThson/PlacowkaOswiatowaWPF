using Microsoft.EntityFrameworkCore;
using PlacowkaOswiatowa.Domain.Interfaces.RepositoryInterfaces;
using PlacowkaOswiatowa.Domain.Models.Base;
using PlacowkaOswiatowa.Infrastructure.DataAccess;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace PlacowkaOswiatowa.Infrastructure.Repository
{
    public class PlacowkaRepository : IPlacowkaRepository, IDisposable
    {
        private readonly AplikacjaDbContext _dbContext;
        private IAdresRepository _adresy;
        private IPracownikRepository _pracownicy;
        private IUczenRepository _uczniowie;
        private IOddzialRepository _oddzialy;
        private IUrlopRepository _urlopy;
        private IEtatRepository _etaty;
        private IStanowiskoRepository _stanowiska;
        private IPrzedmiotRepository _przedmioty;
        private IOcenaRepository _oceny;
        private IUmowaRepository _umowy;
        private IPracodawcaRepository _pracodawcy;
        private IRolaRepository _role;
        private IUzytkownikRepository _uzytkownicy;

        public IAdresRepository Adresy => _adresy ??= new AdresRepository(_dbContext);
        public IPracownikRepository Pracownicy => _pracownicy ??= new PracownikRepository(_dbContext);
        public IUczenRepository Uczniowie => _uczniowie ??= new UczenRepository(_dbContext);
        public IOddzialRepository Oddzialy => _oddzialy ??= new OddzialRepository(_dbContext);
        public IUrlopRepository Urlopy => _urlopy ??= new UrlopRepository(_dbContext);
        public IEtatRepository Etaty => _etaty ??= new EtatRepository(_dbContext);
        public IStanowiskoRepository Stanowiska => _stanowiska ??= new StanowiskoRepository(_dbContext);
        public IPrzedmiotRepository Przedmioty => _przedmioty ??= new PrzedmiotRepository(_dbContext);
        public IOcenaRepository Oceny => _oceny ??= new OcenaRepository(_dbContext);
        public IUmowaRepository Umowy => _umowy ??= new UmowaRepository(_dbContext);
        public IPracodawcaRepository Pracodawcy => _pracodawcy ??= new PracodawcaRepository(_dbContext);
        public IRolaRepository Role => _role ??= new RolaRepository(_dbContext);
        public IUzytkownikRepository Uzytkownicy => _uzytkownicy ??= new UzytkownikRepository(_dbContext);

        public PlacowkaRepository(AplikacjaDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public bool CanConnect() =>
            _dbContext.Database.CanConnect();

        public async Task SaveAsync() =>
            await _dbContext.SaveChangesAsync();

        public void Save() => _dbContext.SaveChanges();

        public void Dispose() => _dbContext.Dispose();

        public T GetByName<T>(string name)
            where T : BaseDictionaryEntity<int> =>
            _dbContext.Set<T>().FirstOrDefault(e => e.Nazwa == name);

        public T GetById<T>(int id)
            where T : BaseEntity<int> =>
            _dbContext.Set<T>().FirstOrDefault(e => e.Id == id);

        public async Task AddAsync<T>(T entity)
            where T : class =>
            await _dbContext.Set<T>().AddAsync(entity);

        public void Delete<T>(T entity)
            where T : BaseEntity<int>
        {
            if (_dbContext.Entry(entity).State == EntityState.Detached)
                _dbContext.Set<T>().Attach(entity);

            entity.CzyAktywny = false;
        }

        public void Update<T>(T entity)
            where T : class
        {
            _dbContext.Set<T>().Attach(entity);
            _dbContext.Entry(entity).State = EntityState.Modified;
        }
    }
}
