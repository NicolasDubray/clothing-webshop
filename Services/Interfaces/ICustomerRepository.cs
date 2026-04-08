using Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Interfaces
{
    public interface ICustomerRepository : IRepository<Customer>
    {
        Task<Customer?> GetWithOrdersAsync(int id);

        public Task<Customer?> GetWithAddressesAsync(int id);
    }
}
