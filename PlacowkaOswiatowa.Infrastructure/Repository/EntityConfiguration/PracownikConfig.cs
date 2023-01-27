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
        }
    }
}
