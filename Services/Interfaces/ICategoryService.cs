using Entities;

namespace Services.Interfaces;

public interface ICategoryService
{
    Task<List<Category>> GetAllAsync();
    Task<Category?> GetByIdAsync(int id);
    Task<List<Category>> SearchByNameAsync(string nameQuery);

    void Add(Category category);
    void Update(Category category);
    void Delete(Category category);
}
