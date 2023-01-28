using PlacowkaOswiatowa.Domain.Interfaces.RepositoryInterfaces;
using System;
using System.ComponentModel.DataAnnotations;

namespace PlacowkaOswiatowa.Domain.DTOs
{
    public class UrlopDto : IBaseEntity<object>
    {
        public object Id { get; set; }
        public PracownikDto Pracownik { get; set; }
        public DateTime PoczatekUrlopu { get; set; }
        public DateTime KoniecUrlopu { get; set; }
        public string ZastepujacyPracownik { get; set; }
        public string PrzyczynaUrlopu { get; set; }
    }
}
