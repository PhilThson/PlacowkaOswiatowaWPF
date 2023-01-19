using PlacowkaOswiatowa.Domain.Interfaces.RepositoryInterfaces;
using System;

namespace PlacowkaOswiatowa.Domain.DTOs
{
    public class UrlopPracownikaDto : IBaseEntity<object>
    {
        public object Id { get; set; }
        public PracownikDto Pracownik { get; set; }
        public DateTime PoczatekUrlopu { get; set; }
        public DateTime KoniecUrlopu { get; set; }
    }
}
