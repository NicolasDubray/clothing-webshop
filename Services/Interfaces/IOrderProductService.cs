using Entities;

namespace Services.Interfaces;

public interface IOrderProductService
{
    Task<List<OrderProduct>> GetAllAsync();
    Task<OrderProduct?> GetByIdAsync(int id);
    Task<List<OrderProduct>> GetByOrderIdAsync(int orderId);

    Task AddAsync(OrderProduct orderProduct);
    Task UpdateAsync(OrderProduct orderProduct);
    Task DeleteAsync(OrderProduct orderProduct);
}
