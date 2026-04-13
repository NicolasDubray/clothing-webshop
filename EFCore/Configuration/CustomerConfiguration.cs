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
                new Customer { Id = 1, Name = "Erik Eriksson", BirthDate = new DateTime(1998, 01, 01), Email = "erik.eriksson@gmail.se", Phone = "0705679901" },
                new Customer { Id = 2, Name = "Maja Ek", BirthDate = new DateTime(2002, 05, 08), Email = "majaek@gmail.se", Phone = "0705586547" },
                new Customer { Id = 3, Name = "Lasse Olsson", BirthDate = new DateTime(1972, 09, 20), Email = "lasse@hotmail.se", Phone = "0765004545" },
                new Customer { Id = 4, Name = "Frida Bengtsson", BirthDate = new DateTime(1990, 08, 02), Email = "frida1990@gmail.com", Phone = "0703441919" },
                new Customer { Id = 5, Name = "Ronny Davidsson", BirthDate = new DateTime(1972, 04, 18), Email = "ronny.david@hotmail.se", Phone = "0756789900"},
                new Customer { Id = 6, Name = "Vera Eriksson", BirthDate = new DateTime(1980, 09, 08), Email = "vera.eriksson@hotmail.se", Phone = "0708908888"}
                );
        }
    }
}
