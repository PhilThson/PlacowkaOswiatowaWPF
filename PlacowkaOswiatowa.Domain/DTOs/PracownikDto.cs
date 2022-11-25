using PlacowkaOswiatowa.Domain.Models;
using System;

namespace PlacowkaOswiatowa.Domain.DTOs
{
    public class PracownikDto
    {
        //Warstwa pośrednicząca przydatna do wyświetlania danych
        //np. format DateTime można przekonwertować na string
        //nie trzeba zawierać wszystkich danych
        //można dokonywać sprawdzeń
        public int Id { get; set; }
        public string Imie { get; set; }
        public string DrugieImie { get; set; }
        public string Nazwisko { get; set; }
        public DateTime? DataUrodzenia { get; set; }
        public string Pesel { get; set; }
        public int DniUrlopu { get; set; }
        public double? WymiarGodzinowy { get; set; }
        public double? Nadgodziny { get; set; }
        public decimal PensjaBrutto { get; set; }
        public string NrTelefonu { get; set; }
        public string Email { get; set; }
        public DateTime? DataZatrudnienia { get; set; }
        public DateTime? DataKoncaZatrudnienia { get; set; }
        public Stanowisko Stanowisko { get; set; }
        public Etat Etat { get; set; }
        public AdresDto Adres { get; set; }
    }
}
