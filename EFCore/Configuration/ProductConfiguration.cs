using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Data;

namespace EFCore.Configuration
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {

            builder.Property(p => p.Name)
                .HasMaxLength(64);

            builder.Property(p => p.ShortDescription)
                .HasMaxLength(100);

            builder.Property(p => p.LongDescription)
                .HasMaxLength(500);

            builder.Property(p => p.Price)
                .HasColumnType(SqlDbType.Money.ToString());

            builder.HasData(
                new Product { Id = 1, Name = "White Slim Fit Jeans", Price = 60, OnSale = false, ShortDescription = "Clean and crisp white jeans for any casual occasion.", LongDescription = "Made from 98% cotton and 2% elastane. Machine wash at 30°C, do not tumble dry. Iron inside out on low heat. Not water resistant.", BrandId = 1, CategoryId = 1 },
                new Product { Id = 2, Name = "Black Suit Trousers in Cotton", Price = 80, OnSale = false, ShortDescription = "Elegant black trousers perfect for the office or a night out.", LongDescription = "Crafted from 100% premium cotton. Dry clean recommended. Iron on medium heat. Tailored fit with a straight leg cut.", BrandId = 2, CategoryId = 1 },
                new Product { Id = 3, Name = "Grey Relaxed Chinos", Price = 55, OnSale = false, ShortDescription = "Comfortable everyday chinos with a modern relaxed fit.", LongDescription = "Made from soft cotton blend. Machine wash at 40°C. Tumble dry on low. Iron on medium heat. Versatile enough for both casual and smart casual looks.", BrandId = 2, CategoryId = 1 },
                new Product { Id = 4, Name = "Navy Blue Cargo Pants", Price = 60, OnSale = false, ShortDescription = "Functional cargo pants with plenty of pocket space.", LongDescription = "Made from durable ripstop fabric, 100% cotton. Machine wash at 30°C. Do not tumble dry. Water resistant coating on the outer layer.", BrandId = 1, CategoryId = 1 },
                new Product { Id = 5, Name = "Dark Wash Straight Leg Jeans", Price = 70, OnSale = true, ShortDescription = "A timeless dark wash jean that goes with everything.", LongDescription = "95% cotton, 5% elastane. Machine wash at 30°C inside out to preserve color. Do not bleach. Iron on low heat.", BrandId = 1, CategoryId = 1 },

                new Product { Id = 6, Name = "White Oxford Button-Down Shirt", Price = 45, OnSale = false, ShortDescription = "A classic white Oxford shirt for a clean everyday look.", LongDescription = "100% cotton Oxford weave. Machine wash at 40°C. Iron on medium to high heat while slightly damp for best results. Not water resistant.", BrandId = 4, CategoryId = 2 },
                new Product { Id = 7, Name = "Slim Fit Striped Dress Shirt", Price = 50, OnSale = false, ShortDescription = "Sharp striped dress shirt for a polished office look.", LongDescription = "Made from 100% cotton. Machine wash at 30°C. Iron on medium heat. Slim fit cut designed to be tucked in.", BrandId = 4, CategoryId = 2 },
                new Product { Id = 8, Name = "Oversized Flannel Shirt", Price = 40, OnSale = false, ShortDescription = "Cozy flannel shirt perfect for layering on cooler days.", LongDescription = "100% brushed cotton flannel. Machine wash at 40°C. Tumble dry on low. Iron on medium heat. Can be worn open as a light jacket.", BrandId = 2, CategoryId = 2 },
                new Product { Id = 9, Name = "Black Linen Shirt", Price = 45, OnSale = false, ShortDescription = "Breathable linen shirt ideal for warm weather.", LongDescription = "55% linen, 45% cotton. Machine wash at 30°C or hand wash. Do not tumble dry. Iron on medium heat while damp. Lightweight and breathable.", BrandId = 2, CategoryId = 2 },
                new Product { Id = 10, Name = "Denim Shirt Regular Fit", Price = 47, OnSale = true, ShortDescription = "Versatile denim shirt that works as a shirt or light jacket.", LongDescription = "100% cotton denim. Machine wash at 30°C inside out to preserve color. Do not bleach. Iron on medium heat.", BrandId = 1, CategoryId = 2 },

                new Product { Id = 11, Name = "Black Puffer Jacket", Price = 13, OnSale = false, ShortDescription = "Warm and lightweight puffer jacket for cold days.", LongDescription = "Shell: 100% polyester. Filling: 80% down, 20% feather. Machine wash at 30°C on gentle cycle. Water resistant outer shell. Suitable for temperatures down to -10°C.", BrandId = 3, CategoryId = 3 },
                new Product { Id = 12, Name = "Navy Bomber Jacket", Price = 10, OnSale = true, ShortDescription = "Classic bomber jacket with a modern slim fit.", LongDescription = "100% nylon shell with polyester lining. Hand wash or dry clean only. Water resistant. Not suitable for heavy rain. Iron on low heat on reverse side only.", BrandId = 2, CategoryId = 3 },
                new Product { Id = 13, Name = "Khaki Field Jacket", Price = 11, OnSale = false, ShortDescription = "Rugged field jacket with multiple pockets for outdoor adventures.", LongDescription = "65% polyester, 35% cotton. Machine wash at 30°C. Water resistant coating. Suitable for light rain. Do not tumble dry. Iron on low heat.", BrandId = 3, CategoryId = 3 },
                new Product { Id = 14, Name = "Wool Blend Overcoat", Price = 200, OnSale = false, ShortDescription = "Elegant overcoat perfect for a smart winter look.", LongDescription = "60% wool, 40% polyester. Dry clean only. Not water resistant — avoid heavy rain. Iron on low heat with a damp cloth. Provides excellent insulation in cold weather.", BrandId = 4, CategoryId = 3 },
                new Product { Id = 15, Name = "Windbreaker Jacket", Price = 85, OnSale = false, ShortDescription = "Lightweight windbreaker ideal for outdoor activities.", LongDescription = "100% recycled polyester. Machine wash at 30°C. Highly wind resistant and water repellent. Packable into its own pocket. Great for hiking and running.", BrandId = 3, CategoryId = 3 },

                new Product { Id = 16, Name = "White Leather Sneakers", Price = 90, OnSale = false, ShortDescription = "Clean white leather sneakers that go with almost anything.", LongDescription = "Upper: 100% leather. Sole: rubber. Wipe clean with a damp cloth. Use leather conditioner to maintain quality. Not suitable for heavy rain or wet conditions.", BrandId = 3, CategoryId = 4 },
                new Product { Id = 17, Name = "Black Running Shoes", Price = 120, OnSale = false, ShortDescription = "Lightweight running shoes with excellent cushioning.", LongDescription = "Mesh upper for breathability. Rubber outsole with grip pattern for various surfaces. Machine washable at 30°C in a laundry bag. Remove insoles before washing. Not waterproof.", BrandId = 3, CategoryId = 4 },
                new Product { Id = 18, Name = "Brown Chelsea Boots", Price = 150, OnSale = false, ShortDescription = "Classic Chelsea boots that elevate any outfit.", LongDescription = "Upper: genuine leather. Sole: rubber. Clean with a damp cloth and leather conditioner. Water resistant but avoid prolonged exposure to rain. Polish regularly to maintain finish.", BrandId = 4, CategoryId = 4 },
                new Product { Id = 19, Name = "Grey Slip-On Sneakers", Price = 70, OnSale = false, ShortDescription = "Easy slip-on sneakers for effortless everyday wear.", LongDescription = "Canvas upper, rubber sole. Machine wash at 30°C in a laundry bag. Air dry only, do not tumble dry. Lightweight and flexible for all-day comfort.", BrandId = 2, CategoryId = 4 },
                new Product { Id = 20, Name = "High-Top Canvas Sneakers", Price = 70, OnSale = false, ShortDescription = "Iconic high-top sneakers with a streetwear edge.", LongDescription = "100% cotton canvas upper, rubber sole. Machine wash at 30°C. Air dry only. Not water resistant. Available in multiple colorways.", BrandId = 3, CategoryId = 4 }
                );
        }
    }
}
