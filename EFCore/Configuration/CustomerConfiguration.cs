using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EFCore.Configuration
{
    public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {

            builder.Property(c => c.Name)
                .HasMaxLength(64);

            builder.Property(c => c.Email)
                .HasMaxLength(64);

            builder.Property(c => c.Phone)
                .HasMaxLength(64);
        }

    }
}
