using Entities;

using Microsoft.EntityFrameworkCore;

using Services.Interfaces;

namespace EFCore.Repositories;

public class OrderRepository(WebshopDbContext context)
    : Repository<Order>(context), IOrderRepository
{
    public Task<List<Order>> GetByCustomerIdAsync(int customerId)
    {
        return context.Orders
            .AsNoTracking()
            .Where(o => o.CustomerId == customerId)
            .ToListAsync();
    }
}
