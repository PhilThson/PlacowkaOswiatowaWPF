using PlacowkaOswiatowa.Domain.Models;

namespace PlacowkaOswiatowa.Domain.DTOs
{
    public class CreateUzytkownikDto
    {
        public int Id { get; set; }
        public string Imie { get; set; }
        public string Nazwisko { get; set; }
        public string Email { get; set; }
        public string Haslo { get; set; }
        public string PowtorzHaslo { get; set; }
        public string HashHasla { get; set; }
        public Rola Rola { get; set; }
    }
}
