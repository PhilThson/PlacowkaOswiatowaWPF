using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PlacowkaOswiatowa.Domain.Models;

namespace PlacowkaOswiatowa.Infrastructure.Repository.EntityConfiguration
{
    public class PracownikConfig : IEntityTypeConfiguration<Pracownik>
    {
        public void Configure(EntityTypeBuilder<Pracownik> builder)
        {
            builder.HasQueryFilter(p => p.CzyAktywny);

            builder
                .Property(u => u.ObliczoneWynagrodzenieNetto)
                .HasColumnType("money")
                .HasPrecision(8, 2)
                .IsRequired(false);

            builder
                .Property(u => u.ObliczonaStawkaNaGodzineNetto)
                .HasColumnType("money")
                .HasPrecision(6, 2)
                .IsRequired(false);
        }
    }
}
