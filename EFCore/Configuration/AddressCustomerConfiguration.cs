using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace EFCore.Configuration
{
    public class AddressCustomerConfiguration : IEntityTypeConfiguration<AddressCustomer>
    {
        public void Configure(EntityTypeBuilder<AddressCustomer> builder)
        {
            builder.HasData(
                new AddressCustomer { Id = 1, AddressId = 1, CustomerId = 1}
                );
        }
    }
}
