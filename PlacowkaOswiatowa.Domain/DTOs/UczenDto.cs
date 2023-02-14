using PlacowkaOswiatowa.Domain.Interfaces.RepositoryInterfaces;
using System;

namespace PlacowkaOswiatowa.Domain.DTOs
{
    public class UczenDto : IBaseEntity<object>
    {
        public object Id { get; set; }
        public string Imie { get; set; }
        public string DrugieImie { get; set; }
        public string Nazwisko { get; set; }
        public string Pesel { get; set; }
        public DateTime? DataUrodzenia { get; set; }
        public AdresDto Adres { get; set; }
        public PracownikDto Wychowawca { get; set; }
        public OddzialDto Oddzial { get; set; }
    }
}
