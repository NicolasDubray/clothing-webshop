using Entities;

namespace Services.Interfaces;

public interface IProductService
{
    Task<List<Product>> GetAllAsync();
    Task<Product?> GetByIdAsync(int id);
    Task<List<Product>> SearchAsync(string query);

    void Add(Product product);
    void Update(Product product);
    void Delete(Product product);
}
