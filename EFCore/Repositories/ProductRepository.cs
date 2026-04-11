using Entities;

using Microsoft.EntityFrameworkCore;

using Services.Interfaces;

namespace EFCore.Repositories;

public class ProductRepository(WebshopDbContext context) : Repository<Product>(context), IProductRepository
{
    public Task<Product?> GetAllDetailsAsync(int id)
    {
        return context.Products
            .Include(p => p.Brand)
            .Include(p => p.Category)
            .FirstOrDefaultAsync(p => p.Id == id);
    }

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
}
