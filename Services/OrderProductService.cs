using Entities;

using Services.Interfaces;

namespace Services;

public class OrderProductService(IOrderProductRepository orderProductRepository) : IOrderProductService
{
    public Task<List<OrderProduct>> GetAllAsync()
        => orderProductRepository.GetAllAsync();

    public Task<OrderProduct?> GetByIdAsync(int id)
        => orderProductRepository.GetByIdAsync(id);

    public Task<List<OrderProduct>> GetByOrderIdAsync(int orderId)
        => orderProductRepository.GetByOrderIdAsync(orderId);

    public Task AddAsync(OrderProduct orderProduct)
        => orderProductRepository.AddAsync(orderProduct);

    public Task UpdateAsync(OrderProduct orderProduct)
        => orderProductRepository.UpdateAsync(orderProduct);

    public Task DeleteAsync(OrderProduct orderProduct)
        => orderProductRepository.DeleteAsync(orderProduct);
}
