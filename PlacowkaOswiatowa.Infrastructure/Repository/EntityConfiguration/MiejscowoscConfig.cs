using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PlacowkaOswiatowa.Domain.Models;
using System;

namespace PlacowkaOswiatowa.Infrastructure.Repository.EntityConfiguration
{
    public class MiejscowoscConfig : IEntityTypeConfiguration<Miejscowosc>
    {
        public void Configure(EntityTypeBuilder<Miejscowosc> builder)
        {
            builder.HasIndex(m => m.Nazwa).IsUnique();
        }
    }
}
