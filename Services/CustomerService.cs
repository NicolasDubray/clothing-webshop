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

        public void Add(Customer customer)
            => customerRepository.Add(customer);

        public void Update(Customer customer)
            => customerRepository.Update(customer);

        public void Delete(Customer customer)
            => customerRepository.Delete(customer);
    }
}
