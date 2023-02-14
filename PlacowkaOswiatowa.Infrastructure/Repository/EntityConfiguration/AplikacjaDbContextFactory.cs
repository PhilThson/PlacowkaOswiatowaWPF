using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using PlacowkaOswiatowa.Infrastructure.DataAccess;
using System;

namespace PlacowkaOswiatowa.Infrastructure.Repository.EntityConfiguration
{
    //Ta klasa jest potrzebna do migracji
    //W razie rezygnacji z niej będzie rzucany wyjątek, że DbContext jako singleton
    //jest wstrzykiwany do klas będących Transient'ami (ew. na odwrót)
    public class AplikacjaDbContextFactory : IDesignTimeDbContextFactory<AplikacjaDbContext>
    {
        private readonly string _connectionString;

        //musi też być konstruktor bez parametryczny
        public AplikacjaDbContextFactory()
        { }

        public AplikacjaDbContextFactory(string connectionString)
        {
            _connectionString = connectionString;
        }

        public AplikacjaDbContext CreateDbContext(string[] args)
        {
            var options = new DbContextOptionsBuilder<AplikacjaDbContext>();
            //options.LogTo(Console.WriteLine);
            //nie można wstrzyknąć konfiguracji, bo rzuca błędem przy rejestracji serwisów
            options.UseSqlServer("Server=.;Database=PlacowkaOswiatowaDb;Trusted_Connection=True;");
            return new AplikacjaDbContext(options.Options);
        }
    }
}
