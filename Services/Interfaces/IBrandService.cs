using Entities;

namespace Services.Interfaces;

public interface IBrandService
{
    Task<List<Brand>> GetAllAsync();
    Task<Brand?> GetByIdAsync(int id);
    Task<List<Brand>> SearchByNameAsync(string nameQuery);

    Task AddAsync(Brand brand);
    Task UpdateAsync(Brand brand);
    Task DeleteAsync(Brand brand);
}
