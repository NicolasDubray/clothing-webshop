using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EFCore.Configuration
{
    public class BrandConfiguration : IEntityTypeConfiguration<Brand>
    {
        public void Configure(EntityTypeBuilder<Brand> builder)
        {

            builder.Property(b => b.Name)
                .HasMaxLength(64);

            builder.HasData(
                new Brand { Id = 1, Name = "Levi´s"},
                new Brand { Id = 2, Name = "Weekday"},
                new Brand { Id = 3, Name = "Nike"},
                new Brand { Id = 4, Name = "Tommy Hilfiger"}
                );
        }

    }
}
