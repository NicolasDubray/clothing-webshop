using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Data;

namespace EFCore.Configuration
{
    public class ShippingConfiguration : IEntityTypeConfiguration<Shipping>
    {
        public void Configure(EntityTypeBuilder<Shipping> builder)
        {

            builder.Property(s => s.Name)
                .HasMaxLength(64);

            builder.Property(s => s.Price)
                .HasColumnType(SqlDbType.Money.ToString());

        }
    }
}