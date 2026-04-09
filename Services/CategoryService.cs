using Entities;

using Services.Interfaces;

namespace Services;

public class CategoryService(ICategoryRepository categoryRepository) : ICategoryService
{
    public Task<List<Category>> GetAllAsync()
        => categoryRepository.GetAllAsync();

    public Task<Category?> GetByIdAsync(int id)
        => categoryRepository.GetByIdAsync(id);

    public Task<List<Category>> SearchByNameAsync(string nameQuery)
        => categoryRepository.SearchByNameAsync(nameQuery);
    public Task<List<Category>> GetBestSellingCategoriesAsync(int count)
        => categoryRepository.GetBestSellingCategoriesAsync(count);

    public Task AddAsync(Category category)
        => categoryRepository.AddAsync(category);

    public Task UpdateAsync(Category category)
        => categoryRepository.UpdateAsync(category);

    public Task DeleteAsync(Category category)
        => categoryRepository.DeleteAsync(category);
}
