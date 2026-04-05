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
}
