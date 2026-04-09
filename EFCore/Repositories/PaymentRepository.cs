using Entities;

using Services.Interfaces;

namespace EFCore.Repositories;

public class PaymentRepository(WebshopDbContext context)
    : Repository<Payment>(context), IPaymentRepository
{
}
