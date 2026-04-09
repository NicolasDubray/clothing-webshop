using Entities;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services
{
    public class CustomerService(ICustomerRepository customerRepository) : ICustomerService
    {
        public Task<List<Customer>> GetAllAsync()
        => customerRepository.GetAllAsync();

        public Task<Customer?> GetByIdAsync(int id)
            => customerRepository.GetByIdAsync(id);

        public Task<Customer?> GetWithOrdersAsync(int id)
            => customerRepository.GetWithOrdersAsync(id);

        public Task<Customer?> GetWithAddressesAsync(int id)
            => customerRepository.GetWithAddressesAsync(id);
        public Task<List<Customer>> GetTopBuyingCustomersAsync(int count)
            => customerRepository.GetTopBuyingCustomersAsync(count);
        public Task AddAsync(Customer customer)
            => customerRepository.AddAsync(customer);

        public Task UpdateAsync(Customer customer)
            => customerRepository.UpdateAsync(customer);

        public Task DeleteAsync(Customer customer)
            => customerRepository.DeleteAsync(customer);
    }
}
