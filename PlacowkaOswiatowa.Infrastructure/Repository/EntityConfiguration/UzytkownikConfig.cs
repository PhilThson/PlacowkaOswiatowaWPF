using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PlacowkaOswiatowa.Domain.Models;

namespace PlacowkaOswiatowa.Infrastructure.Repository.EntityConfiguration
{
    public class UzytkownikConfig : IEntityTypeConfiguration<Uzytkownik>
    {
        public void Configure(EntityTypeBuilder<Uzytkownik> builder)
        {
            builder.HasIndex(u => u.Email).IsUnique();

            builder
                .HasOne(u => u.Rola)
                .WithMany(r => r.RolaUzytkownicy)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
