using Entities;

using Microsoft.EntityFrameworkCore;

using Services.Interfaces;

namespace EFCore.Repositories;

public class ProductRepository(WebshopDbContext context) : Repository<Product>(context), IProductRepository
{
    public Task<List<Product>> SearchAsync(string query)
    {
        return context.Products
            .AsNoTracking()
            .Where(p =>
                p.Name.Contains(query) ||
                p.ShortDescription.Contains(query) ||
                p.LongDescription.Contains(query) ||
                p.Brand.Name.Contains(query) ||
                p.Category.Name.Contains(query))
            .ToListAsync();
    }

    public Task<List<Product>> GetProductsWithDealsAsync()
    {
        return context.Products
            .Where(p => p.OnSale == true)
            .ToListAsync();
    }
}
