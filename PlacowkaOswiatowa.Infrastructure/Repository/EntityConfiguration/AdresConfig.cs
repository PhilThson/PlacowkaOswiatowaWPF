using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PlacowkaOswiatowa.Domain.Models;

namespace PlacowkaOswiatowa.Infrastructure.Repository.EntityConfiguration
{
    public class AdresConfig : IEntityTypeConfiguration<Adres>
    {
        public void Configure(EntityTypeBuilder<Adres> builder)
        {
            builder
                .HasOne(a => a.Panstwo)
                .WithMany(p => p.PanstwoAdresy)
                .OnDelete(DeleteBehavior.NoAction);

            builder
                .HasOne(a => a.Miejscowosc)
                .WithMany(p => p.MiejscowoscAdresy)
                .OnDelete(DeleteBehavior.NoAction);

            builder
                .HasOne(a => a.Ulica)
                .WithMany(p => p.UlicaAdresy)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasQueryFilter(a => a.CzyAktywny);
        }
    }
}
