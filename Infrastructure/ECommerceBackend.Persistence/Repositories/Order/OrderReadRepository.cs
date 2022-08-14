using ECommerceBackend.Application.Repositories;
using ECommerceBackend.Domain.Entities.Concrete;
using ECommerceBackend.Persistence.Contexts;

namespace ECommerceBackend.Persistence.Repositories;

public class OrderReadRepository : ReadRepository<Order>, IOrderReadRepository
{
    public OrderReadRepository(ECommerceBackendDbContext context) : base(context)
    {
    }
}