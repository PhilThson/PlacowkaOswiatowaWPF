using PlacowkaOswiatowa.Domain.Interfaces.RepositoryInterfaces;
using PlacowkaOswiatowa.Domain.Models;
using System;

namespace PlacowkaOswiatowa.Domain.DTOs
{
    public class PracownikDto : IBaseEntity<object>
    {
        public object Id { get; set; }
        public string Imie { get; set; }
        public string DrugieImie { get; set; }
        public string Nazwisko { get; set; }
        public DateTime? DataUrodzenia { get; set; }
        public string Pesel { get; set; }
        public int DniUrlopu { get; set; }
        public double? WymiarGodzinowy { get; set; }
        public double? Nadgodziny { get; set; }
        public decimal WynagrodzenieBrutto { get; set; }
        public string NrTelefonu { get; set; }
        public string Email { get; set; }
        public DateTime? DataRozpoczeciaPracy { get; set; }
        public DateTime? DataZakonczeniaPracy { get; set; }
        public Stanowisko Stanowisko { get; set; }
        public Etat Etat { get; set; }
        public AdresDto Adres { get; set; }
        public DateTime? DataZawarciaUmowy { get; set; }
        public string OkresPracy { get; set; }
    }
}
