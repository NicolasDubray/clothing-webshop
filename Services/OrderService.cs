using Entities;

using Services.Interfaces;

namespace Services;

public class OrderService(IOrderRepository orderRepository) : IOrderService
{
    public Task<List<Order>> GetAllAsync()
        => orderRepository.GetAllAsync();

    public Task<Order?> GetByIdAsync(int id)
        => orderRepository.GetByIdAsync(id);

    public Task<List<Order>> GetByCustomerIdAsync(int customerId)
        => orderRepository.GetByCustomerIdAsync(customerId);

    public Task AddAsync(Order order)
        => orderRepository.AddAsync(order);

    public Task UpdateAsync(Order order)
        => orderRepository.UpdateAsync(order);

    public Task DeleteAsync(Order order)
        => orderRepository.DeleteAsync(order);
}
