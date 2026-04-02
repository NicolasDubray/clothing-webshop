using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Data;

namespace EFCore.Configuration
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {

            builder.Property(p => p.Name)
                .HasMaxLength(64);

            builder.Property(p => p.ShortDescription)
                .HasMaxLength(100);

            builder.Property(p => p.LongDescription)
                .HasMaxLength(500);

            builder.Property(p => p.Price)
                .HasColumnType(SqlDbType.Money.ToString());

        }
    }
}
