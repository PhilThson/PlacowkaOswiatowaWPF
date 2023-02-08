using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace PlacowkaOswiatowa.Domain.Models
{
    public class Uczen : Osoba
    {
        public Uczen()
        {
            UczenOceny = new HashSet<Ocena>();
        }

        public byte OddzialId { get; set; }

        [Required]
        [ForeignKey(nameof(OddzialId))]
        [InverseProperty("OddzialUczniowie")]
        public virtual Oddzial Oddzial { get; set; }

        public int? AdresId { get; set; }

        [ForeignKey(nameof(AdresId))]
        [InverseProperty("AdresUczniowie")]
        public virtual Adres Adres { get; set; }

        public virtual ICollection<Ocena> UczenOceny { get; set; }


        public static bool operator ==(Uczen u1, Uczen u2)
        {
            if (u1 is null) return false;
            if (u2 is null) return false;
            if ((Osoba)u1 != (Osoba)u2)
                return false;

            return u1.OddzialId == u2.OddzialId;
        }

        public static bool operator !=(Uczen u1, Uczen u2) =>
            !(u1 == u2);
    }
}
