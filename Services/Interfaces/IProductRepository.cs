using Entities;

namespace Services.Interfaces;

public interface IProductRepository : IRepository<Product>
{
    Task<List<Product>> SearchAsync(string query);
}
