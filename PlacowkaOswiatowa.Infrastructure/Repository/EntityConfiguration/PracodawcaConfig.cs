using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PlacowkaOswiatowa.Domain.Models;

namespace PlacowkaOswiatowa.Infrastructure.Repository.EntityConfiguration
{
    public class PracodawcaConfig : IEntityTypeConfiguration<Pracodawca>
    {
        public void Configure(EntityTypeBuilder<Pracodawca> builder)
        {
            builder.HasIndex(p => p.Nazwa).IsUnique();
        }
    }
}
