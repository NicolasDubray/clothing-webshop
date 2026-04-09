using Entities;

namespace Services.Interfaces;

public interface ICategoryService
{
    Task<List<Category>> GetAllAsync();
    Task<Category?> GetByIdAsync(int id);
    Task<List<Category>> SearchByNameAsync(string nameQuery);
    Task<List<Category>> GetBestSellingCategoriesAsync(int count);
    Task AddAsync(Category category);
    Task UpdateAsync(Category category);
    Task DeleteAsync(Category category);
}
