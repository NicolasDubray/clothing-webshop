using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EFCore.Configuration
{


    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {

            builder.Property(c => c.Name)
                .HasMaxLength(64);

            builder.HasData(

                new Category { Id = 1, Name = "Pants"},
                new Category { Id = 1, Name = "Shirt"},
                new Category { Id = 1, Name = "Jacket"},
                new Category { Id = 1, Name = "Shoes"}
                );
        }

    }
}

