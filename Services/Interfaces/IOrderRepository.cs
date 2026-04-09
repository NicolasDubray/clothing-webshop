using Entities;

namespace Services.Interfaces;

public interface IOrderRepository : IRepository<Order>
{
    Task<List<Order>> GetByCustomerIdAsync(int customerId);
}
