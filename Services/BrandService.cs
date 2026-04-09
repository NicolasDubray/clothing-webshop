using Entities;

using Services.Interfaces;

namespace Services;

public class BrandService(IBrandRepository brandRepository) : IBrandService
{
    public Task<List<Brand>> GetAllAsync()
        => brandRepository.GetAllAsync();

    public Task<Brand?> GetByIdAsync(int id)
        => brandRepository.GetByIdAsync(id);

    public Task<List<Brand>> SearchByNameAsync(string nameQuery)
        => brandRepository.SearchByNameAsync(nameQuery);

    public Task AddAsync(Brand brand)
        => brandRepository.AddAsync(brand);

    public Task UpdateAsync(Brand brand)
        => brandRepository.UpdateAsync(brand);

    public Task DeleteAsync(Brand brand)
        => brandRepository.DeleteAsync(brand);
}
