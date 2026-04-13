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
                new Order { Id = 4, OrderNumber = "1001037849", CustomerId = 1, PaymentId = 3, ShippingId = 2 },
                new Order { Id = 5, OrderNumber = "1001045646", CustomerId = 2, PaymentId = 3, ShippingId = 1 },
                new Order { Id = 6, OrderNumber = "1001012482", CustomerId = 2, PaymentId = 3, ShippingId = 1 },
                new Order { Id = 7, OrderNumber = "1001014742", CustomerId = 3, PaymentId = 1, ShippingId = 1 },
                new Order { Id = 8, OrderNumber = "1001014455", CustomerId = 4, PaymentId = 1, ShippingId = 1 },
                new Order { Id = 9, OrderNumber = "1001054587", CustomerId = 4, PaymentId = 2, ShippingId = 2 },
                new Order { Id = 10, OrderNumber = "1001057719", CustomerId = 5, PaymentId = 2, ShippingId = 2 },
                new Order { Id = 11, OrderNumber = "1001046466", CustomerId = 5, PaymentId = 3, ShippingId = 1 },
                new Order { Id = 12, OrderNumber = "1001078419", CustomerId = 6, PaymentId = 1, ShippingId = 2 },
                new Order { Id = 13, OrderNumber = "1001038888", CustomerId = 6, PaymentId = 2, ShippingId = 1 },
                new Order { Id = 14, OrderNumber = "1001031243", CustomerId = 6, PaymentId = 1, ShippingId = 2 },
                new Order { Id = 15, OrderNumber = "1001010100", CustomerId = 6, PaymentId = 2, ShippingId = 1 }
                );
        }
    }
}