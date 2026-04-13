using Entities;

using Microsoft.EntityFrameworkCore;

using Services.Interfaces;

namespace EFCore.Repositories;

public class CategoryRepository(WebshopDbContext context) : Repository<Category>(context), ICategoryRepository
{
    public Task<List<Category>> SearchByNameAsync(string nameQuery)
    {
        return context.Categories
            .AsNoTracking()
            .Where(c => c.Name.Contains(nameQuery))
            .ToListAsync();
    }

    public async Task<List<Category>> GetBestSellingCategoriesAsync(int count)
    {
        var topCategoryIds = await context.OrderProducts
            .GroupBy(op => op.Product.CategoryId)
            .OrderByDescending(g => g.Sum(op => op.ProductAmount))
            .Select(g => g.Key)
            .Take(count)
            .ToListAsync();

        return await context.Categories
            .Where(c => topCategoryIds.Contains(c.Id))
            .ToListAsync();
    }
}
