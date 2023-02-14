using PlacowkaOswiatowa.Domain.Models.Base;
using System.Collections.Generic;

namespace PlacowkaOswiatowa.Domain.Models
{
    public class Etat : BaseDictionaryEntity<byte>
    {
        public Etat()
        {
            EtatUmowy = new HashSet<Umowa>();
        }

        public virtual ICollection<Umowa> EtatUmowy { get; set; }
    }
}
