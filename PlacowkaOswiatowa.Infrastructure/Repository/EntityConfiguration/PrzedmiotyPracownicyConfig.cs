﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PlacowkaOswiatowa.Domain.Models;

namespace PlacowkaOswiatowa.Infrastructure.Repository.EntityConfiguration
{
    public class PrzedmiotyPracownicyConfig : IEntityTypeConfiguration<PrzedmiotyPracownicy>
    {
        public void Configure(EntityTypeBuilder<PrzedmiotyPracownicy> builder)
        {
            builder.HasKey(pp => new { pp.PracownikId, pp.PrzedmiotId });

            builder
                .HasOne(pp => pp.Pracownik)
                .WithMany(p => p.PracownikPrzedmiotyPracownicy)
                .OnDelete(DeleteBehavior.NoAction);

            builder
                .HasOne(pp => pp.Przedmiot)
                .WithMany(p => p.PrzedmiotPrzedmiotyPracownicy)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
