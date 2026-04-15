using Entities;
using Microsoft.EntityFrameworkCore;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace EFCore.Repositories
{
    public class CustomerRepository(WebshopDbContext context) : Repository<Customer>(context), ICustomerRepository
    {
        public Task<Customer?> GetWithOrdersAsync(int id)
        {
            return context.Customers
            .Include(c => c.Orders)
                .ThenInclude(o => o.OrderProducts)
                    .ThenInclude(op => op.Product)
            .AsNoTracking()
            .FirstOrDefaultAsync(c => c.Id == id);
        }

        public Task<Customer?> GetWithAddressesAsync(int id)
        {
            return context.Customers
                .Include(c => c.Addresses)
                    .ThenInclude(ac => ac.Address)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<List<Customer>> GetTopBuyingCustomersAsync(int count)
        {
            var topCustomerIds = await context.OrderProducts
                .GroupBy(op => op.Order.CustomerId)
                .OrderByDescending(g => g.Sum(op => op.Product.Price * op.ProductAmount))
                .Select(g => g.Key)
                .Take(count)
                .ToListAsync();

            var customers = await context.Customers
                    .Where(c => topCustomerIds.Contains(c.Id))
                    .ToListAsync();

            return customers.OrderBy(c => topCustomerIds.IndexOf(c.Id)).ToList();
        }
    }
}
