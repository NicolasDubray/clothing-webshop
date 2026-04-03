using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EFCore.Configuration
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {

            builder.Property(o => o.OrderNumber)
                .HasMaxLength(20);

            builder.HasData(
                new Order { Id = 1, OrderNumber = "1001050608"},
                new Order { Id = 2, OrderNumber = "1001050609"},
                new Order { Id = 3, OrderNumber = "1001070819"},
                new Order { Id = 4, OrderNumber = "1001037849"}
                );
        }
    }
}