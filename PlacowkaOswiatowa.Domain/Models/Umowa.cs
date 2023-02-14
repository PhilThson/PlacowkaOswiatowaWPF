using PlacowkaOswiatowa.Domain.Models.Base;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlacowkaOswiatowa.Domain.Models
{
    public class Umowa : BaseEntity<int>
    {
        public int PracownikId { get; set; }

        [ForeignKey(nameof(PracownikId))]
        [InverseProperty("PracownikUmowa")]
        public virtual Pracownik Pracownik { get; set; }

        public byte PracodawcaId { get; set; }

        [ForeignKey(nameof(PracodawcaId))]
        [InverseProperty("PracodawcaUmowy")]
        public virtual Pracodawca Pracodawca { get; set; }

        [Range(0.0, 99999.0)]
        public decimal WynagrodzenieBrutto { get; set; }

        public bool CzyZwolnionyOdPodatku { get; set; }

        [MaxLength(128)]
        public string OpisWynagrodzenia { get; set; }

        [MaxLength(256)]
        public string MiejsceWykonywaniaPracy { get; set; }

        [MaxLength(32)]
        public string WymiarCzasuPracy { get; set; }

        public double? WymiarGodzinowy { get; set; }

        [MaxLength(32)]
        public string OkresPracy { get; set; }

        [Column(TypeName = "date")]
        public DateTime DataZawarciaUmowy { get; set; }

        public byte EtatId { get; set; }

        [ForeignKey(nameof(EtatId))]
        [InverseProperty("EtatUmowy")]
        public virtual Etat Etat { get; set; }

        public byte StanowiskoId { get; set; }

        [ForeignKey(nameof(StanowiskoId))]
        [InverseProperty("StanowiskoUmowy")]
        public virtual Stanowisko Stanowisko { get; set; }

        [Column(TypeName = "date")]
        public DateTime DataRozpoczeciaPracy { get; set; }

        [Column(TypeName = "date")]
        public DateTime? DataZakonczeniaPracy { get; set; }

        [MaxLength(128)]
        public string InneWarunkiZatrudnienia { get; set; }

        [MaxLength(256)]
        public string PrzyczynyZawarciaUmowy { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime DataUtworzenia { get; set; }
    }
}
