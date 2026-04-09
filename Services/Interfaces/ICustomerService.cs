using Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Interfaces
{
    public interface ICustomerService
    {
        Task<List<Customer>> GetAllAsync();
        Task<Customer?> GetByIdAsync(int id);
        Task<Customer?> GetWithOrdersAsync(int id);
        public Task<Customer?> GetWithAddressesAsync(int id);
        Task<List<Customer>> GetTopBuyingCustomersAsync(int count);
        void Add(Customer customer);
        void Update(Customer customer);
        void Delete(Customer customer);
    }
}
