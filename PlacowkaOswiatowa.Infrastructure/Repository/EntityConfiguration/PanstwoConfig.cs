using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PlacowkaOswiatowa.Domain.Models;

namespace PlacowkaOswiatowa.Infrastructure.Repository.EntityConfiguration
{
    public class PanstwoConfig : IEntityTypeConfiguration<Panstwo>
    {
        public void Configure(EntityTypeBuilder<Panstwo> builder)
        {
            builder.HasIndex(p => p.Nazwa).IsUnique();
        }
    }
}
