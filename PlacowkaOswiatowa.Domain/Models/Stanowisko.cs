using PlacowkaOswiatowa.Domain.Models.Base;
using System.Collections.Generic;

namespace PlacowkaOswiatowa.Domain.Models
{
    public class Stanowisko : BaseDictionaryEntity<byte>
    {
        public Stanowisko()
        {
            StanowiskoUmowy = new HashSet<Umowa>();
        }

        public virtual ICollection<Umowa> StanowiskoUmowy { get; set; }
    }
}
