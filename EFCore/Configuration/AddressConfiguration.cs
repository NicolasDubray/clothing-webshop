using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EFCore.Configuration
{


    public class AddressConfiguration : IEntityTypeConfiguration<Address>
    {
        public void Configure(EntityTypeBuilder<Address> builder)
        {

            builder.Property(a => a.StreetAddress)
                .HasMaxLength(64);

            builder.Property(a => a.City)
                .HasMaxLength(64);

            builder.Property(a => a.Country)
                .HasMaxLength(64);

            builder.HasData(
                new Address { Id = 1,  StreetAddress = "Uddevallagatan 1", City = "Uddevalla", Country = "Sweden"}
                );
        }

    }
}
