using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using PlacowkaOswiatowa.Domain.Models.Base;
using static PlacowkaOswiatowa.Domain.Extensions.CommonExtensions;

namespace PlacowkaOswiatowa.Domain.Models
{
    public abstract class Osoba : BaseEntity<int>
    {
        [Required]
        [MaxLength(20)]
        public string Imie { get; set; }

        [MaxLength(20)]
        public string DrugieImie { get; set; }

        [Required]
        [MaxLength(50)]
        public string Nazwisko { get; set; }

        [MaxLength(10)]
        [Column(TypeName = "date")]
        public DateTime? DataUrodzenia { get; set; }

        [Required]
        [MaxLength(11)]
        [Column(TypeName = "varchar(11)")]
        public string Pesel { get; set; }

        public static bool operator ==(Osoba o1, Osoba o2)
        {
            if (o1 is null && o2 is null) return true;
            if (o1 is null) return false;
            if (o2 is null) return false;
            //return o1.GetHashCode() == o2.GetHashCode();
            return o1.Imie.ToLower() == o2.Imie.ToLower() &&
               SafeToLower(o1.DrugieImie) == SafeToLower(o2.DrugieImie) &&
               o1.Nazwisko.ToLower() == o2.Nazwisko.ToLower() &&
               ((o1.DataUrodzenia.HasValue ? o1.DataUrodzenia.Value.Date : DateTime.Today) ==
               (o2.DataUrodzenia.HasValue ? o2.DataUrodzenia.Value.Date : DateTime.Today)) &&
               o1.Pesel == o2.Pesel;
        }
        public static bool operator !=(Osoba o1, Osoba o2)
        {
            return !(o1 == o2);
        }
        public override bool Equals(object obj)
        {
            if (!(obj is Osoba)) return false;
            Osoba o = (Osoba)obj;
            return (this == o);
        }
        public override int GetHashCode()
        {
            return Imie.GetHashCode() ^ Nazwisko.GetHashCode() ^ Pesel.GetHashCode();
        }
    }
}
