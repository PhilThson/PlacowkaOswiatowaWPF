using Bogus;
using PlacowkaOswiatowa.Domain.Models;
using PlacowkaOswiatowa.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PlacowkaOswiatowa.Infrastructure.Repository.EntityConfiguration
{
    public sealed class Seeder
    {
        #region Prywatne właściwości
        private static List<Adres> Adresy { get; set; } =
            new List<Adres>();
        private static List<Etat> Etaty { get; set; } =
            new List<Etat>();
        private static List<Miejscowosc> Miejscowosci { get; set; } =
            new List<Miejscowosc>();
        private static List<Ocena> Oceny { get; set; } =
            new List<Ocena>();
        private static List<Oddzial> Oddzialy { get; set; } =
            new List<Oddzial>();
        private static List<Panstwo> Panstwa { get; set; } =
            new List<Panstwo>();
        private static List<PracownicyAdresy> PracownicyAdresy { get; set; } =
            new List<PracownicyAdresy>();
        private static List<Pracownik> Pracownicy { get; set; } =
            new List<Pracownik>();
        private static List<Przedmiot> Przedmioty { get; set; } =
            new List<Przedmiot>();
        private static List<PrzedmiotyPracownicy> PrzedmiotyPracownicy { get; set; } =
            new List<PrzedmiotyPracownicy>();
        private static List<Rola> Role { get; set; } =
            new List<Rola>();
        private static List<Stanowisko> Stanowiska { get; set; } =
            new List<Stanowisko>();
        private static List<Uczen> Uczniowie { get; set; } =
            new List<Uczen>();
        private static List<Ulica> Ulice { get; set; } =
            new List<Ulica>();
        private static List<Urlop> Urlopy { get; set; } =
            new List<Urlop>();
        private static List<Uzytkownik> Uzytkownicy { get; set; } =
            new List<Uzytkownik>();
        #endregion

        #region Zasilanie encji słownikowych

        public static List<Etat> EtatySeed()
        {
            var etatFaker = new Faker<Etat>()
                .UseSeed(1122)
                .RuleFor(e => e.Id, f => (byte)(f.IndexFaker + 1))
                .RuleFor(e => e.Nazwa, f => ((EtatEnum)f.IndexFaker).ToString())
                .RuleFor(e => e.CzyAktywny, f => true);

            Etaty = etatFaker.Generate(3);
            return Etaty;
        }

        public static List<Miejscowosc> MiejscowosciSeed()
        {
            var miejscowoscFaker = new Faker<Miejscowosc>()
                .UseSeed(2233)
                .RuleFor(e => e.Id, f => f.IndexFaker + 1)
                .RuleFor(e => e.Nazwa, f => ((MiejscowoscEnum)f.IndexFaker).ToString())
                .RuleFor(e => e.CzyAktywny, f => true);

            Miejscowosci = miejscowoscFaker.Generate(21);
            return Miejscowosci;
        }

        public static List<Panstwo> PanstwaSeed()
        {
            var panstwoFaker = new Faker<Panstwo>()
                .UseSeed(4455)
                .RuleFor(e => e.Id, f => f.IndexFaker + 1)
                .RuleFor(e => e.Nazwa, f => ((PanstwoEnum)f.IndexFaker).ToString())
                .RuleFor(e => e.CzyAktywny, f => true);

            Panstwa = panstwoFaker.Generate(8);
            return Panstwa;
        }

        public static List<Przedmiot> PrzedmiotySeed()
        {
            var przedmiotFaker = new Faker<Przedmiot>()
                .UseSeed(5566)
                .RuleFor(e => e.Id, f => (byte)(f.IndexFaker + 1))
                .RuleFor(e => e.Nazwa, f => ((PrzedmiotEnum)f.IndexFaker).ToString())
                .RuleFor(e => e.CzyAktywny, f => true);

            Przedmioty = przedmiotFaker.Generate(17);
            return Przedmioty;
        }

        public static List<Rola> RoleSeed()
        {
            var rolaFaker = new Faker<Rola>()
                .UseSeed(6677)
                .RuleFor(e => e.Id, f => (byte)(f.IndexFaker + 1))
                .RuleFor(e => e.Nazwa, f => ((RolaEnum)f.IndexFaker).ToString())
                .RuleFor(e => e.CzyAktywny, f => true);

            Role = rolaFaker.Generate(5);
            return Role;
        }

        public static List<Stanowisko> StanowiskaSeed()
        {
            var stanowiskoFaker = new Faker<Stanowisko>()
                .UseSeed(7788)
                .RuleFor(e => e.Id, f => (byte)(f.IndexFaker + 1))
                .RuleFor(e => e.Nazwa, f => ((StanowiskoEnum)f.IndexFaker).ToString())
                .RuleFor(e => e.CzyAktywny, f => true);

            Stanowiska = stanowiskoFaker.Generate(6);
            return Stanowiska;
        }

        public static List<Ulica> UliceSeed()
        {
            var ulicaFaker = new Faker<Ulica>()
                .UseSeed(8899)
                .RuleFor(e => e.Id, f => f.IndexFaker + 1)
                .RuleFor(e => e.Nazwa, f => ((UlicaEnum)f.IndexFaker).ToString())
                .RuleFor(e => e.CzyAktywny, f => true);

            Ulice = ulicaFaker.Generate(10);
            return Ulice;
        }

        #endregion

        #region Zasilanie encji posiadających relacje klucza obcego
        public static List<Adres> AdresySeed()
        {
            var adresFaker = new Faker<Adres>()
                .UseSeed(9911)
                .RuleFor(a => a.Id, f => f.IndexFaker + 1)
                .RuleFor(a => a.PanstwoId, f => f.PickRandom(Panstwa).Id)
                .RuleFor(a => a.MiejscowoscId, f => f.PickRandom(Miejscowosci).Id)
                .RuleFor(a => a.UlicaId, f => f.PickRandom(Ulice).Id)
                .RuleFor(a => a.NumerDomu, f => f.Random.Number(1, 200).ToString())
                .RuleFor(a => a.NumerMieszkania, f => f.Random.Number(1, 200).ToString())
                .RuleFor(a => a.KodPocztowy, f => f.Address.ZipCode("##-###"))
                .RuleFor(a => a.CzyAktywny, f => true);

            Adresy = adresFaker.Generate(200);
            return Adresy;
        }

        public static List<Pracownik> PracownicySeed()
        {
            var pracownikFaker = new Faker<Pracownik>()
                .UseSeed(1133)
                .RuleFor(p => p.Id, f => f.IndexFaker + 1)
                .RuleFor(p => p.Imie, f => f.Person.FirstName)
                .RuleFor(p => p.Nazwisko, f => f.Person.LastName)
                .RuleFor(p => p.DataUrodzenia, f => f.Date.PastOffset(60, DateTime.Now.AddYears(-23)).Date)
                .RuleFor(p => p.Pesel, f => f.Random.ReplaceNumbers("##########"))
                .RuleFor(p => p.NrTelefonu, f => f.Phone.PhoneNumber("###-###-###"))
                .RuleFor(p => p.Email, f => f.Person.Email)
                .RuleFor(p => p.EtatId, f => f.PickRandom(Etaty).Id)
                .RuleFor(p => p.StanowiskoId, f => f.PickRandom(Stanowiska).Id)
                .RuleFor(p => p.WymiarGodzinowy, f => f.Random.Int(15, 40))
                .RuleFor(p => p.Pensja, f => f.Finance.Amount(2800, 10000))
                .RuleFor(p => p.DniUrlopu, f => f.Random.Number(1, 100))
                .RuleFor(p => p.DataZatrudnienia, f => f.Date.PastOffset(10, DateTime.Now.AddDays(-1)).Date)
                .RuleFor(p => p.CzyAktywny, f => true);

            Pracownicy = pracownikFaker.Generate(50);
            return Pracownicy;
        }

        public static List<Oddzial> OddzialySeed()
        {
            int i = 1;
            var oddzialFaker = new Faker<Oddzial>()
                .UseSeed(3344)
                .RuleFor(o => o.Id, f => (byte)(f.IndexFaker + 1))
                .RuleFor(o => o.Nazwa, f => ((OddzialEnum)f.IndexFaker).ToString())
                .RuleFor(o => o.PracownikId, f => i++)
                .RuleFor(o => o.CzyAktywny, f => true);

            Oddzialy = oddzialFaker.Generate(35);
            return Oddzialy;
        }

        public static List<Uczen> UczniowieSeed()
        {
            var uczenFaker = new Faker<Uczen>()
                .UseSeed(2244)
                .RuleFor(u => u.Id, f => f.IndexFaker + 1)
                .RuleFor(u => u.Imie, f => f.Person.FirstName)
                .RuleFor(u => u.Nazwisko, f => f.Person.LastName)
                .RuleFor(u => u.DataUrodzenia, f => f.Date.PastOffset(15, DateTime.Now.AddYears(-3)).Date)
                .RuleFor(u => u.Pesel, f => f.Random.ReplaceNumbers("##########"))
                .RuleFor(u => u.AdresId, f => f.PickRandom(Adresy).Id)
                .RuleFor(u => u.WychowawcaId, f => f.PickRandom(Pracownicy).Id)
                .RuleFor(u => u.OddzialId, f => f.PickRandom(Oddzialy).Id);

            Uczniowie = uczenFaker.Generate(100);
            return Uczniowie;
        }

        #region Zasilanie tabel asocjacyjnych (wiele do wielu)
        public static List<PracownicyAdresy> PracownicyAdresySeed()
        {
            var pracownicyAdresyFaker = new Faker<PracownicyAdresy>()
                .UseSeed(3355)
                .RuleFor(pa => pa.PracownikId, f => f.PickRandom(Pracownicy).Id)
                .RuleFor(pa => pa.AdresId, f => f.PickRandom(Adresy).Id);

            PracownicyAdresy = pracownicyAdresyFaker.Generate(50)
                .GroupBy(pa => new { pa.PracownikId, pa.AdresId }).Select(c => c.FirstOrDefault()).ToList();
            return PracownicyAdresy;
        }

        public static List<PrzedmiotyPracownicy> PrzedmiotyPracownicySeed()
        {
            var przedmiotyPracownicyFaker = new Faker<PrzedmiotyPracownicy>()
                .UseSeed(4466)
                .RuleFor(pp => pp.PracownikId, f => f.PickRandom(Pracownicy).Id)
                .RuleFor(pp => pp.PrzedmiotId, f => f.PickRandom(Przedmioty).Id);

            PrzedmiotyPracownicy = przedmiotyPracownicyFaker.Generate(50)
                .GroupBy(pp => new { pp.PracownikId, pp.PrzedmiotId}).Select(c => c.FirstOrDefault()).ToList();
            return PrzedmiotyPracownicy;
        }
        #endregion

        public static List<Urlop> UrlopySeed()
        {
            //W ten sposób tylko 1 pracownik jest na urlopie w danym czasie
            Urlop urlop = null;

            var urlopFaker = new Faker<Urlop>()
                .UseSeed(5577)
                .RuleFor(u => u.PracownikId, f => f.PickRandom(Pracownicy).Id)
                .RuleFor(u => u.PoczatekUrlopu, f => urlop?.KoniecUrlopu.AddDays(1) ?? DateTime.Parse("2022-01-01"))
                .RuleFor(u => u.KoniecUrlopu, (f, current) => current.PoczatekUrlopu.AddDays(f.Random.Number(1, 10)))
                .RuleFor(u => u.CzyAktywny, f => true)
                .FinishWith((f, current) => urlop = current);

            Urlopy = urlopFaker.Generate(40);
            return Urlopy;
        }

        public static List<Ocena> OcenySeed()
        {
            var ocenaFaker = new Faker<Ocena>()
                .UseSeed(6688)
                .RuleFor(o => o.Id, f => f.IndexFaker + 1)
                .RuleFor(o => o.PracownikId, f => f.PickRandom(Pracownicy).Id)
                .RuleFor(o => o.UczenId, f => f.PickRandom(Uczniowie).Id)
                .RuleFor(o => o.PrzedmiotId, f => f.PickRandom(Przedmioty).Id)
                .RuleFor(o => o.WystawionaOcena, f => f.Random.Decimal(1, 6))
                .RuleFor(o => o.DataWystawienia, f => f.Date.Recent(100))
                .RuleFor(o => o.CzyAktywny, f => true);

            Oceny = ocenaFaker.Generate(100);
            return Oceny;
        }

        public static List<Uzytkownik> UzytkownicySeed()
        {
            Uzytkownicy = new List<Uzytkownik>
            {
                new Uzytkownik
                {
                    Id = 1,
                    Email = "jan@kowalski.mail.com",
                    Imie = "Jan",
                    Nazwisko = "Kowalski",
                    RolaId = 2,
                    HashHasla = "AAA",
                    CzyAktywny = true
                },
                new Uzytkownik
                {
                    Id = 2,
                    Email = "roman@nowak.mail.com",
                    Imie = "Roman",
                    Nazwisko = "Nowak",
                    RolaId = 3,
                    HashHasla = "BBB",
                    CzyAktywny = true
                },
                new Uzytkownik
                {
                    Id = 3,
                    Email = "adam@wisniewski.mail.com",
                    Imie = "Adam",
                    Nazwisko = "Wiśniewski",
                    RolaId = 4,
                    HashHasla = "CCC",
                    CzyAktywny = true
                }
            };

            return Uzytkownicy;
        }
        #endregion
    }
}
