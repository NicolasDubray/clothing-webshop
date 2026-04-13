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
                new OrderProduct { Id = 4, ProductAmount = 3, OrderId = 4, ProductId = 11},
                new OrderProduct { Id = 5, ProductAmount = 1, OrderId = 5, ProductId = 2},
                new OrderProduct { Id = 6, ProductAmount = 2, OrderId = 6, ProductId = 3},
                new OrderProduct { Id = 7, ProductAmount = 3, OrderId = 7, ProductId = 4},
                new OrderProduct { Id = 8, ProductAmount = 1, OrderId = 8, ProductId = 5},
                new OrderProduct { Id = 9, ProductAmount = 2, OrderId = 9, ProductId = 6},
                new OrderProduct { Id = 10, ProductAmount = 3, OrderId = 10, ProductId = 7},
                new OrderProduct { Id = 11, ProductAmount = 2, OrderId = 10, ProductId = 8},
                new OrderProduct { Id = 12, ProductAmount = 1, OrderId = 10, ProductId = 9},
                new OrderProduct { Id = 13, ProductAmount = 1, OrderId = 11, ProductId = 12},
                new OrderProduct { Id = 14, ProductAmount = 1, OrderId = 12, ProductId = 13},
                new OrderProduct { Id = 15, ProductAmount = 2, OrderId = 13, ProductId = 14},
                new OrderProduct { Id = 16, ProductAmount = 2, OrderId = 14, ProductId = 18},
                new OrderProduct { Id = 17, ProductAmount = 5, OrderId = 15, ProductId = 20},
                new OrderProduct { Id = 18, ProductAmount = 3, OrderId = 15, ProductId = 18}
                );
        }
    }
}
