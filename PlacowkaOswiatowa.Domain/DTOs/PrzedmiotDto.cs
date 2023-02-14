using PlacowkaOswiatowa.Domain.Interfaces.RepositoryInterfaces;

namespace PlacowkaOswiatowa.Domain.DTOs
{
    public class PrzedmiotDto : IBaseEntity<object>
    {
        public object Id { get; set; }
        public string Nazwa { get; set; }
        public string Opis { get; set; }
        public string SkroconaNazwa { get; set; }
    }
}
