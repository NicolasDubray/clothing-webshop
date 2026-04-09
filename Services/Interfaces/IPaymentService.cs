using Entities;

namespace Services.Interfaces;

public interface IPaymentService
{
    Task<List<Payment>> GetAllAsync();
    Task<Payment?> GetByIdAsync(int id);

    Task AddAsync(Payment payment);
    Task UpdateAsync(Payment payment);
    Task DeleteAsync(Payment payment);
}
