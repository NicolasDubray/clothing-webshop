using Entities;

namespace Services.Interfaces;

public interface IBrandRepository : IRepository<Brand>
{
    Task<List<Brand>> SearchByNameAsync(string nameQuery);
}
