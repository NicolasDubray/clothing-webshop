using Entities;

using Microsoft.EntityFrameworkCore;

using Services.Interfaces;

namespace EFCore.Repositories;

public class OrderProductRepository(WebshopDbContext context)
    : Repository<OrderProduct>(context), IOrderProductRepository
{
    public Task<List<OrderProduct>> GetByOrderIdAsync(int orderId)
    {
        return context.OrderProducts
            .AsNoTracking()
            .Where(op => op.OrderId == orderId)
            .ToListAsync();
    }
}
