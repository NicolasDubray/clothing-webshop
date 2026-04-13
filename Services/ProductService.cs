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
        
    public Task<List<Product>> GetProductsWithDealsAsync()
        =>productRepository.GetProductsWithDealsAsync();
        
    public Task<List<Product>> GetBestSellingProductsAsync(int count)
        => productRepository.GetBestSellingProductsAsync(count);
        
    public Task<double> GetTotalRevenueAsync()
        => productRepository.GetTotalRevenueAsync();
    public Task<bool> CategoryHasProductsAsync(int categoryId)
        => productRepository.CategoryHasProductsAsync(categoryId);

    public Task<Product?> GetAllDetailsAsync(int id)
        => productRepository.GetAllDetailsAsync(id);

    public Task AddAsync(Product product)
        => productRepository.AddAsync(product);

    public Task UpdateAsync(Product product)
        => productRepository.UpdateAsync(product);

    public Task DeleteAsync(Product product)
        => productRepository.DeleteAsync(product);
}
