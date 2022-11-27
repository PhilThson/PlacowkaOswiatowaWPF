using PlacowkaOswiatowa.Domain.Models.Base;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlacowkaOswiatowa.Domain.Models
{
    public class Pracodawca : BaseEntity<byte>
    {
        public Pracodawca()
        {
            PracodawcaUmowy = new HashSet<Umowa>();
        }

        [MaxLength(128)]
        public string Nazwa { get; set; }

        public int AdresId { get; set; }

        [ForeignKey(nameof(AdresId))]
        [InverseProperty("AdresPracodawca")]
        public virtual Adres Adres { get; set; }

        [Column(TypeName = "char(9)")]
        public string Regon { get; set; }

        public virtual ICollection<Umowa> PracodawcaUmowy { get; set; }
    }
}
