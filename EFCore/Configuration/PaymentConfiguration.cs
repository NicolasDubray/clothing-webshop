using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace EFCore.Configuration
{
    public class PaymentConfiguration : IEntityTypeConfiguration<Payment>
    {
        public void Configure(EntityTypeBuilder<Payment> builder)
        {
            builder.HasData(
                new Payment { Id = 1, Type = Entities.Types.PaymentType.Debit},
                new Payment { Id = 2, Type = Entities.Types.PaymentType.Klarna},
                new Payment { Id = 3, Type = Entities.Types.PaymentType.Swish}
                );
        }
    }
}
