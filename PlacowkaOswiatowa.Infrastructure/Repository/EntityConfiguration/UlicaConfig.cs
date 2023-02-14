using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PlacowkaOswiatowa.Domain.Models;
using System;

namespace PlacowkaOswiatowa.Infrastructure.Repository.EntityConfiguration
{
    public class UlicaConfig : IEntityTypeConfiguration<Ulica>
    {
        public void Configure(EntityTypeBuilder<Ulica> builder)
        {
            builder.HasIndex(u => u.Nazwa).IsUnique();
        }
    }
}
