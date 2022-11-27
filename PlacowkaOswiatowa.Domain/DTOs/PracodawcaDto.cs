using System.ComponentModel.DataAnnotations;

namespace PlacowkaOswiatowa.Domain.DTOs
{
    public class PracodawcaDto
    {
        public byte Id { get; set; }
        [MaxLength(128)]
        public string Nazwa { get; set; }
        public AdresDto Adres { get; set; }
        [MaxLength(9)]
        public string Regon { get; set; }
    }
}
