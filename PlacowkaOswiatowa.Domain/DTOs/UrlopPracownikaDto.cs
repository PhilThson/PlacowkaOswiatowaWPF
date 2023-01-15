using System;

namespace PlacowkaOswiatowa.Domain.DTOs
{
    public class UrlopPracownikaDto
    {
        public PracownikDto Pracownik { get; set; }
        public DateTime PoczatekUrlopu { get; set; }
        public DateTime KoniecUrlopu { get; set; }
    }
}
