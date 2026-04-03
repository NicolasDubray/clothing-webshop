using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace EFCore.Configuration
{
    public class OrderProductConfiguration : IEntityTypeConfiguration<OrderProduct>
    {
        public void Configure(EntityTypeBuilder<OrderProduct> builder)
        {
            builder.HasData(
                new OrderProduct { Id = 1, ProductAmount = 2, OrderId = 1, ProductId = 1 },
                new OrderProduct { Id = 2, ProductAmount = 1, OrderId = 2, ProductId = 5 },
                new OrderProduct { Id = 3, ProductAmount = 1, OrderId = 3, ProductId = 10 },
                new OrderProduct { Id = 4, ProductAmount = 3, OrderId = 4, ProductId = 11}
                );
        }
    }
}
