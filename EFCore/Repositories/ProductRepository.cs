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

    public Task<List<Product>> GetProductsWithDealsAsync()
    {
        return context.Products
            .Where(p => p.OnSale == true)
            .ToListAsync();
    }       
            
    public async Task<List<Product>> GetBestSellingProductsAsync(int count)
    {
        var topProductsIds = await context.OrderProducts
            .GroupBy(op => op.ProductId)
            .OrderByDescending(g => g.Sum(op => op.ProductAmount))
            .Select(g => g.Key)
            .Take(count)
            .ToListAsync();

        return await context.Products
            .Include(p => p.Brand)
            .Include(p => p.Category)
            .Where(p => topProductsIds.Contains(p.Id))
            .ToListAsync();
    }

    public Task<double> GetTotalRevenueAsync()
    {
        return context.OrderProducts
            .Select(op => op.ProductAmount * op.Product.Price)
            .SumAsync();
    }
}
