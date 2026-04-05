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

    public void Add(Category category)
        => categoryRepository.Add(category);

    public void Update(Category category)
        => categoryRepository.Update(category);

    public void Delete(Category category)
        => categoryRepository.Delete(category);
}
