using Entities;

using Services.Interfaces;

namespace Services;

public class ProductService(IProductRepository productRepository) : IProductService
{
    public Task<List<Product>> GetAllAsync()
        => productRepository.GetAllAsync();

    public Task<Product?> GetByIdAsync(int id)
        => productRepository.GetByIdAsync(id);

    public Task<List<Product>> SearchAsync(string query)
        => productRepository.SearchAsync(query);
    public Task<List<Product>> GetBestSellingProductsAsync(int count)
        => productRepository.GetBestSellingProductsAsync(count);
    public Task<double> GetTotalRevenueAsync()
        => productRepository.GetTotalRevenueAsync();

    public void Add(Product product)
        => productRepository.Add(product);

    public void Update(Product product)
        => productRepository.Update(product);

    public void Delete(Product product)
        => productRepository.Delete(product);
}
