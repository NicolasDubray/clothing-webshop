using Entities;

using Microsoft.EntityFrameworkCore;

using Services.Interfaces;

namespace EFCore.Repositories;

public class BrandRepository(WebshopDbContext context) : Repository<Brand>(context), IBrandRepository
{
    public Task<List<Brand>> SearchByNameAsync(string nameQuery)
    {
        return context.Brands
            .AsNoTracking()
            .Where(b => b.Name.Contains(nameQuery))
            .ToListAsync();
    }
}
