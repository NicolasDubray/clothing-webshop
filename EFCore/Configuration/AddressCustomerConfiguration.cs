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
                new AddressCustomer { Id = 1, AddressId = 1, CustomerId = 1},
                new AddressCustomer { Id = 2, AddressId = 2, CustomerId = 2},
                new AddressCustomer { Id = 3, AddressId = 3, CustomerId = 3},
                new AddressCustomer { Id = 4, AddressId = 4, CustomerId = 4},
                new AddressCustomer { Id = 5, AddressId = 5, CustomerId = 5},
                new AddressCustomer { Id = 6, AddressId = 6, CustomerId = 6}
                );
        }
    }
}
