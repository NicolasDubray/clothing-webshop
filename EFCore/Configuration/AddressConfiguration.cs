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
                new Address { Id = 1,  StreetAddress = "Uddevallagatan 1", City = "Uddevalla", Country = "Sweden"},
                new Address { Id = 2,  StreetAddress = "Biblioteksgatan 5", City = "Stockholm", Country = "Sweden"},
                new Address { Id = 3,  StreetAddress = "Kaserngården 6", City = "Uddevalla", Country = "Sweden"},
                new Address { Id = 4,  StreetAddress = "Göteborgsgatan 15", City = "Göteborg", Country = "Sweden"},
                new Address { Id = 5,  StreetAddress = "Hallerna Hills 20", City = "Stenungsund", Country = "Sweden"},
                new Address { Id = 6,  StreetAddress = "Pressarevägen", City = "Göteborg", Country = "Sweden"}
                );
        }

    }
}
