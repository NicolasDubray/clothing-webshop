using Entities;

namespace Services.Interfaces;

public interface IProductRepository : IRepository<Product>
{
    Task<List<Product>> SearchAsync(string query);

    Task<List<Product>> GetProductsWithDealsAsync();
    Task<List<Product>> GetBestSellingProductsAsync(int count);
    Task<double> GetTotalRevenueAsync();
}
