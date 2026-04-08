using Entities;

namespace Services.Interfaces;

public interface IBrandService
{
    Task<List<Brand>> GetAllAsync();
    Task<Brand?> GetByIdAsync(int id);
    Task<List<Brand>> SearchByNameAsync(string nameQuery);

    void Add(Brand brand);
    void Update(Brand brand);
    void Delete(Brand brand);
}
