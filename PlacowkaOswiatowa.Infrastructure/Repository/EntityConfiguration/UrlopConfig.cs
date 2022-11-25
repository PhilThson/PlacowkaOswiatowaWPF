using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PlacowkaOswiatowa.Domain.Models;

namespace PlacowkaOswiatowa.Infrastructure.Repository.EntityConfiguration
{
    public class UrlopConfig : IEntityTypeConfiguration<Urlop>
    {
        public void Configure(EntityTypeBuilder<Urlop> builder)
        {
            builder.HasKey(u => new { u.PracownikId, u.PoczatekUrlopu });
        }
    }
}
