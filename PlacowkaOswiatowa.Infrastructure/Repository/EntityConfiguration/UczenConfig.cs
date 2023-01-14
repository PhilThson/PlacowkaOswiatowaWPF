using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PlacowkaOswiatowa.Domain.Models;

namespace PlacowkaOswiatowa.Infrastructure.Repository.EntityConfiguration
{
    public class UczenConfig : IEntityTypeConfiguration<Uczen>
    {
        public void Configure(EntityTypeBuilder<Uczen> builder)
        {
            builder
                .HasOne(u => u.Oddzial)
                .WithMany(o => o.OddzialUczniowie)
                .OnDelete(DeleteBehavior.NoAction);

            builder
                .HasMany(u => u.UczenOceny)
                .WithOne(o => o.Uczen)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
