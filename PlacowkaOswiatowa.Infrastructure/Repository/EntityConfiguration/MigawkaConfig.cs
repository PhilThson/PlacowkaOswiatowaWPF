using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PlacowkaOswiatowa.Domain.Models;

namespace PlacowkaOswiatowa.Infrastructure.Repository.EntityConfiguration
{
    public class MigawkaConfig : IEntityTypeConfiguration<Migawka>
    {
        public void Configure(EntityTypeBuilder<Migawka> builder)
        {
            builder
                .Property(m => m.DataZmiany)
                .HasColumnType("datetime")
                .HasComputedColumnSql("getdate()");

            builder
                .HasOne(m => m.Uzytkownik)
                .WithMany(u => u.UzytkownikMigawki)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
