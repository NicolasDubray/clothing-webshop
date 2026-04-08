using Entities;

namespace Services.Interfaces;

public interface ICategoryRepository : IRepository<Category>
{
    Task<List<Category>> SearchByNameAsync(string nameQuery);
}
