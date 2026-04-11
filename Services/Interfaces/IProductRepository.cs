using Entities;

namespace Services.Interfaces;

public interface IProductRepository : IRepository<Product>
{
    Task<Product?> GetAllDetailsAsync(int id); 
    Task<List<Product>> SearchAsync(string query);
}
