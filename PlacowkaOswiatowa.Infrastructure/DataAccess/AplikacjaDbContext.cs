using Microsoft.EntityFrameworkCore;
using PlacowkaOswiatowa.Domain.Models;
using PlacowkaOswiatowa.Infrastructure.Repository.EntityConfiguration;
using System.Reflection;

namespace PlacowkaOswiatowa.Infrastructure.DataAccess
{
    public class AplikacjaDbContext : DbContext
    {
        public DbSet<Adres> Adresy { get; set; }
        public DbSet<Etat> Etaty { get; set; }
        public DbSet<Miejscowosc> Miejscowosci { get; set; }
        public DbSet<Migawka> Migawki { get; set; }
        public DbSet<Ocena> Oceny { get; set; }
        public DbSet<Oddzial> Oddzialy { get; set; }
        public DbSet<Panstwo> Panstwa { get; set; }
        public DbSet<Pracodawca> Pracodawcy { get; set; }
        public DbSet<Pracownik> Pracownicy { get; set; }
        public DbSet<Przedmiot> Przedmioty { get; set; }
        public DbSet<Rola> Role { get; set; }
        public DbSet<Stanowisko> Stanowiska { get; set; }
        public DbSet<Uczen> Uczniowie { get; set; }
        public DbSet<Ulica> Ulice { get; set; }
        public DbSet<Umowa> Umowy { get; set; }
        public DbSet<Urlop> Urlopy { get; set; }
        public DbSet<Uzytkownik> Uzytkownicy { get; set; }


        public AplikacjaDbContext(DbContextOptions options) : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            //modelBuilder.Entity<Etat>().HasData(Seeder.EtatySeed());
            //modelBuilder.Entity<Miejscowosc>().HasData(Seeder.MiejscowosciSeed());
            //modelBuilder.Entity<Panstwo>().HasData(Seeder.PanstwaSeed());
            //modelBuilder.Entity<Przedmiot>().HasData(Seeder.PrzedmiotySeed());
            //modelBuilder.Entity<Rola>().HasData(Seeder.RoleSeed());
            //modelBuilder.Entity<Stanowisko>().HasData(Seeder.StanowiskaSeed());
            //modelBuilder.Entity<Ulica>().HasData(Seeder.UliceSeed());
            //modelBuilder.Entity<Adres>().HasData(Seeder.AdresySeed());
            //modelBuilder.Entity<Pracownik>().HasData(Seeder.PracownicySeed());
            //modelBuilder.Entity<Pracodawca>().HasData(Seeder.PracodawcySeed());
            ////Oddział wymaga pracownika
            //modelBuilder.Entity<Oddzial>().HasData(Seeder.OddzialySeed());
            ////Uczen wymaga oddziału
            //modelBuilder.Entity<Uczen>().HasData(Seeder.UczniowieSeed());
            //modelBuilder.Entity<Umowa>().HasData(Seeder.UmowySeed());
            //modelBuilder.Entity<PracownicyAdresy>().HasData(Seeder.PracownicyAdresySeed());
            //modelBuilder.Entity<PrzedmiotyPracownicy>().HasData(Seeder.PrzedmiotyPracownicySeed());
            //modelBuilder.Entity<Urlop>().HasData(Seeder.UrlopySeed());
            //modelBuilder.Entity<Ocena>().HasData(Seeder.OcenySeed());
            //modelBuilder.Entity<Uzytkownik>().HasData(Seeder.UzytkownicySeed());
        }
    }
}
