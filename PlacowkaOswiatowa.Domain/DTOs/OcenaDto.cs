using PlacowkaOswiatowa.Domain.Interfaces.RepositoryInterfaces;
using System;

namespace PlacowkaOswiatowa.Domain.DTOs
{
    public class OcenaDto : IBaseEntity<object>
    {
        public object Id { get; set; }
        public decimal WystawionaOcena { get; set; }
        public UczenDto Uczen { get; set; }
        public PrzedmiotDto Przedmiot { get; set; }
        public PracownikDto Pracownik { get; set; }
        public DateTime DataWystawienia { get; set; }
        public decimal? PoprzedniaOcena { get; set; }
        public DateTime? DataPoprawienia { get; set; }
    }
}
