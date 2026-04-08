using Entities;

using Services.Interfaces;

namespace Services;

public class PaymentService(IPaymentRepository paymentRepository) : IPaymentService
{
    public Task<List<Payment>> GetAllAsync()
        => paymentRepository.GetAllAsync();

    public Task<Payment?> GetByIdAsync(int id)
        => paymentRepository.GetByIdAsync(id);

    public Task AddAsync(Payment payment)
        => paymentRepository.AddAsync(payment);

    public Task UpdateAsync(Payment payment)
        => paymentRepository.UpdateAsync(payment);

    public Task DeleteAsync(Payment payment)
        => paymentRepository.DeleteAsync(payment);
}
