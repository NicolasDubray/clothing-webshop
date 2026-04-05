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

    public void Add(Brand brand)
        => brandRepository.Add(brand);

    public void Update(Brand brand)
        => brandRepository.Update(brand);

    public void Delete(Brand brand)
        => brandRepository.Delete(brand);
}
