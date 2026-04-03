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

            builder.HasData(
                new Shipping { Id = 1, Name = "Postnord", Price = 4},
                new Shipping { Id = 1, Name = "Bring", Price = 2}
                );

        }
    }
}