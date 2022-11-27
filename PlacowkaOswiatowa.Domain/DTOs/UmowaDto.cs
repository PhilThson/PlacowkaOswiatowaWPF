using PlacowkaOswiatowa.Domain.Models;
using System;
using System.ComponentModel.DataAnnotations;

namespace PlacowkaOswiatowa.Domain.DTOs
{
    public class UmowaDto
    {
        public PracownikDto Pracownik { get; set; }
        public PracodawcaDto Pracodawca { get; set; }
        public decimal WynagrodzenieBrutto { get; set; }
        public bool CzyZwolnionyOdPodatku { get; set; }
        [MaxLength(128)]
        public string OpisWynagrodzenia { get; set; }
        [MaxLength(64)]
        public string MiejsceWykonywaniaPracy { get; set; }
        [MaxLength(32)]
        public string WymiarCzasuPracy { get; set; }
        public double? WymiarGodzinowy { get; set; }
        [MaxLength(32)]
        public string OkresPracy { get; set; }
        public DateTime? DataZawarciaUmowy { get; set; }
        public Etat Etat { get; set; }
        public Stanowisko Stanowisko { get; set; }
        public DateTime? DataRozpoczeciaPracy { get; set; }
        public DateTime? DataZakonczeniaPracy { get; set; }
        [MaxLength(128)]
        public string InneWarunkiZatrudnienia { get; set; }
        [MaxLength(256)]
        public string PrzyczynyZawarciaUmowy { get; set; }
    }
}
