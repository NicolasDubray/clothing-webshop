using Entities;

using Services.Interfaces;

namespace EFCore.Repositories;

public class ShippingRepository(WebshopDbContext context)
    : Repository<Shipping>(context), IShippingRepository
{
}
