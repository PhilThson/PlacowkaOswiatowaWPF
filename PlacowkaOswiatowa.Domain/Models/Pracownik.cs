﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlacowkaOswiatowa.Domain.Models
{
    public class Pracownik : Osoba
    {
        public Pracownik()
        {
            PracownikUrlopy = new HashSet<Urlop>();
            PracownikOceny = new HashSet<Ocena>();
            PracownikPracownicyAdresy = new HashSet<PracownicyAdresy>();
            PracownikPrzedmiotyPracownicy = new HashSet<PrzedmiotyPracownicy>();
        }

        [Range(0, 500)]
        public int? DniUrlopu { get; set; }

        [Column(TypeName = "varchar(11)")]
        public string NrTelefonu { get; set; }

        [MaxLength(50)]
        public string Email { get; set; }

        public decimal? ObliczoneWynagrodzenieNetto { get; set; }

        public decimal? ObliczonaStawkaNaGodzineNetto { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime? DataOstatnichObliczen { get; set; }

        public string ParametryObliczen { get; set; }

        public virtual Umowa PracownikUmowa { get; set; }
        public virtual Oddzial PracownikOddzial { get; set; }
        public virtual ICollection<Urlop> PracownikUrlopy { get; set; }
        public virtual ICollection<Ocena> PracownikOceny { get; set; }
        public virtual ICollection<PracownicyAdresy> PracownikPracownicyAdresy 
        { get; set; }
        public virtual ICollection<PrzedmiotyPracownicy> PracownikPrzedmiotyPracownicy 
        { get; set; }
    }
}
