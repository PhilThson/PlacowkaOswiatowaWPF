﻿using System;

namespace PlacowkaOswiatowa.Domain.DTOs
{
    public class UczenDto
    {
        public int Id { get; set; }
        public string Imie { get; set; }
        public string DrugieImie { get; set; }
        public string Nazwisko { get; set; }
        public string Pesel { get; set; }
        public DateTime? DataUrodzenia { get; set; }
        public string NazwaGrupy { get; set; }
        public string ImieNazwiskoWychowawcy { get; set; }
        public AdresDto Adres { get; set; }
        public PracownikDto Wychowawca { get; set; }
        public OddzialDto Oddzial { get; set; }
    }
}
