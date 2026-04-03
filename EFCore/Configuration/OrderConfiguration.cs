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
                new Order { Id = 1, OrderNumber = "1001050608", CustomerId = 1, PaymentId = 1, ShippingId = 1 },
                new Order { Id = 2, OrderNumber = "1001050609", CustomerId = 1, PaymentId = 2, ShippingId = 2 },
                new Order { Id = 3, OrderNumber = "1001070819", CustomerId = 1, PaymentId = 2, ShippingId = 1 },
                new Order { Id = 4, OrderNumber = "1001037849", CustomerId = 1, PaymentId = 3, ShippingId = 2 }
                );
        }
    }
}