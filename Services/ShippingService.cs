using Entities;

using Services.Interfaces;

namespace Services;

public class ShippingService(IShippingRepository shippingRepository) : IShippingService
{
    public Task<List<Shipping>> GetAllAsync()
        => shippingRepository.GetAllAsync();

    public Task<Shipping?> GetByIdAsync(int id)
        => shippingRepository.GetByIdAsync(id);

    public Task AddAsync(Shipping shipping)
        => shippingRepository.AddAsync(shipping);

    public Task UpdateAsync(Shipping shipping)
        => shippingRepository.UpdateAsync(shipping);

    public Task DeleteAsync(Shipping shipping)
        => shippingRepository.DeleteAsync(shipping);
}
