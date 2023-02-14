using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PlacowkaOswiatowa.Domain.Models;

namespace PlacowkaOswiatowa.Infrastructure.Repository.EntityConfiguration
{
    public class PracownicyAdresyConfig : IEntityTypeConfiguration<PracownicyAdresy>
    {
        public void Configure(EntityTypeBuilder<PracownicyAdresy> builder)
        {
            builder.HasKey(pa => new { pa.PracownikId, pa.AdresId });
        }
    }
}
