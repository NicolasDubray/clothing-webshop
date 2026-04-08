using Entities;

namespace Services.Interfaces;

public interface IOrderProductRepository : IRepository<OrderProduct>
{
    Task<List<OrderProduct>> GetByOrderIdAsync(int orderId);
}
