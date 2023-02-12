using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using PlacowkaOswiatowa.Domain.DTOs;
using PlacowkaOswiatowa.Domain.Models.Base;
using static PlacowkaOswiatowa.Domain.Extensions.CommonExtensions;
using PlacowkaOswiatowa.Domain.Extensions;

namespace PlacowkaOswiatowa.Domain.Models
{
    public class Adres : BaseEntity<int>
    {
        public Adres()
        {
            AdresUczniowie = new HashSet<Uczen>();
            AdresPracownicyAdresy = new HashSet<PracownicyAdresy>();
        }

        public int PanstwoId { get; set; }

        [ForeignKey(nameof(PanstwoId))]
        [InverseProperty("PanstwoAdresy")]
        public virtual Panstwo Panstwo { get; set; }

        public int MiejscowoscId { get; set; }

        [ForeignKey(nameof(MiejscowoscId))]
        [InverseProperty("MiejscowoscAdresy")]
        public virtual Miejscowosc Miejscowosc { get; set; }

        public int? UlicaId { get; set; }

        [ForeignKey(nameof(UlicaId))]
        [InverseProperty("UlicaAdresy")]
        public virtual Ulica Ulica { get; set; }

        [MaxLength(10)]
        public string NumerDomu { get; set; }

        [MaxLength(10)]
        public string NumerMieszkania { get; set; }

        [MaxLength(10)]
        public string KodPocztowy { get; set; }

        public virtual Pracodawca AdresPracodawca { get; set; }
        public virtual ICollection<Uczen> AdresUczniowie { get; set; }
        public virtual ICollection<PracownicyAdresy> AdresPracownicyAdresy 
        { get; set; }

        public static bool operator ==(Adres a1, AdresDto a2)
        {
            if (a1 is null) return false;
            if (a2 is null) return false;

            return SafeToLower(a1.Panstwo?.Nazwa) == a2.Panstwo.ToLowerString() &&
                SafeToLower(a1.Miejscowosc?.Nazwa) == a2.Miejscowosc.ToLowerString() &&
                SafeToLower(a1.Ulica?.Nazwa) == a2.Ulica.ToLower() &&
                a1.NumerDomu.ToLower() == a2.NumerDomu.ToLower() &&
                a1.NumerMieszkania?.ToLower() == a2.NumerMieszkania?.ToLower() &&
                a1.KodPocztowy.ToLower() == a2.KodPocztowy.ToLower();
        }

        public static bool operator !=(Adres a1, AdresDto a2) => !(a1 == a2);

        public static bool operator ==(Adres a1, Adres a2)
        {
            if (a1 is null) return false;
            if (a2 is null) return false;
            return
                SafeToLower(a1.Panstwo?.Nazwa) == SafeToLower(a2.Panstwo?.Nazwa) &&
                SafeToLower(a1.Miejscowosc?.Nazwa) == SafeToLower(a2.Miejscowosc?.Nazwa) &&
                SafeToLower(a1.Ulica?.Nazwa) == SafeToLower(a2.Ulica?.Nazwa) &&
                a1.NumerDomu.ToLowerString() == a2.NumerDomu.ToLowerString() &&
                a1.NumerMieszkania?.ToLowerString() == a2.NumerMieszkania.ToLowerString() &&
                a1.KodPocztowy.ToLowerString() == a2.KodPocztowy.ToLowerString();

            //return a1.GetHashCode() == a2.GetHashCode();

            //if (a1 is null)
            //{
            //    return a2 is null;
            //}

            //return a1.Equals(a2);
        }
        public static bool operator !=(Adres a1, Adres a2) => !(a1 == a2);
        
        public override bool Equals(object obj)
        {
            if (!(obj is Adres)) return false;
            return this == (Adres)obj;

            //if (obj is null)
            //    return false;
            //if (Object.ReferenceEquals(this, obj))
            //    return true;
            //if (this.GetType() != obj.GetType())
            //    return false;
        }
        public override int GetHashCode()
        {
            return PanstwoId ^ MiejscowoscId ^ UlicaId.GetValueOrDefault() ^ (NumerDomu ?? "").GetHashCode() ^ (NumerMieszkania ?? "").GetHashCode();
        }

        public override string ToString()
        {
            var addressDescription = new StringBuilder();
            if (Panstwo != null && !string.IsNullOrEmpty(Panstwo.Nazwa))
                addressDescription.Append(Panstwo.Nazwa);
            if (Ulica != null && !string.IsNullOrEmpty(Ulica.Nazwa))
                addressDescription.Append($", ul. {Ulica}");
            if (!string.IsNullOrEmpty(NumerDomu))
                addressDescription.Append($" {NumerDomu}");
            if (!string.IsNullOrEmpty(NumerMieszkania))
                addressDescription.Append($"/{NumerMieszkania}");
            if (!string.IsNullOrEmpty(KodPocztowy))
                addressDescription.Append($", {KodPocztowy}");
            if (Miejscowosc != null && !string.IsNullOrEmpty(Miejscowosc.Nazwa))
                addressDescription.Append($" {Miejscowosc.Nazwa}");

            return addressDescription.ToString();
        }
    }
}
