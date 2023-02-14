using System;
using System.Collections.Generic;

namespace PlacowkaOswiatowa.Domain.DTOs
{
    public class CreatePracownikDto
    {
        public int Id { get; set; }
        public string Imie { get; set; }
        public string DrugieImie { get; set; }
        public string Nazwisko { get; set; }
        public DateTime? DataUrodzenia { get; set; }
        public string Pesel { get; set; }
        public string NrTelefonu { get; set; }
        public string Email { get; set; }
        public List<AdresDto> Adresy { get; set; }
    }
}
