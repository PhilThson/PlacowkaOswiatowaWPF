using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PlacowkaOswiatowa.Domain.Models;

namespace PlacowkaOswiatowa.Infrastructure.Repository.EntityConfiguration
{
    public class UmowaConfig : IEntityTypeConfiguration<Umowa>
    {
        public void Configure(EntityTypeBuilder<Umowa> builder)
        {
            builder
                .Property(u => u.DataUtworzenia)
                .HasComputedColumnSql("getdate()");

            builder
                .Property(u => u.WynagrodzenieBrutto)
                .HasColumnType("money")
                .HasPrecision(5, 2)
                .IsRequired(true);

            builder
                .HasOne(u => u.Pracodawca)
                .WithMany(p => p.PracodawcaUmowy)
                .OnDelete(DeleteBehavior.NoAction);

            builder
                .HasOne(u => u.Pracownik)
                .WithOne(p => p.PracownikUmowa)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
