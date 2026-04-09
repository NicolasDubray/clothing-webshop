using Entities;

namespace Services.Interfaces;

public interface IShippingService
{
    Task<List<Shipping>> GetAllAsync();
    Task<Shipping?> GetByIdAsync(int id);

    Task AddAsync(Shipping shipping);
    Task UpdateAsync(Shipping shipping);
    Task DeleteAsync(Shipping shipping);
}
