using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Data;

namespace EFCore.Configuration
{
    public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {

            builder.Property(c => c.Name)
                .HasMaxLength(64);

            builder.Property(c => c.BirthDate)
                .HasColumnType(SqlDbType.Date.ToString());

            builder.Property(c => c.Email)
                .HasMaxLength(64);

            builder.Property(c => c.Phone)
                .HasMaxLength(64);

            builder.HasData(
                new Customer { Id = 1, Name = "Erik Eriksson", BirthDate = new DateTime(1998, 01, 01), Email = "erik.eriksson@gmail.se", Phone = "0705679901" }
                );
        }

    }
}
