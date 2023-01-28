using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlacowkaOswiatowa.Domain.Models
{
    public class Urlop
    {
        public int PracownikId { get; set; }

        [ForeignKey(nameof(PracownikId))]
        [InverseProperty("PracownikUrlopy")]
        public virtual Pracownik Pracownik { get; set; }

        [Column(TypeName = "date")]
        public DateTime PoczatekUrlopu { get; set; }

        [Column(TypeName = "date")]
        public DateTime KoniecUrlopu { get; set; }

        [StringLength(128)]
        public string ZastepujacyPracownik { get; set; }

        [StringLength(64)]
        public string PrzyczynaUrlopu { get; set; }

        public bool CzyAktywny { get; set; }
    }
}
