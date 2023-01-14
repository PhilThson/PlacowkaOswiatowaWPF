using System;

namespace PlacowkaOswiatowa.Domain.DTOs
{
    public class CreatePracownikDto
    {
        public string Imie { get; set; }
        public string DrugieImie { get; set; }
        public string Nazwisko { get; set; }
        public DateTime? DataUrodzenia { get; set; }
        public string Pesel { get; set; }
        public string NrTelefonu { get; set; }
        public string Email { get; set; }
        public AdresDto Adres { get; set; }
    }
}
