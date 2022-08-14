using ECommerceBackend.Application.Repositories;
using ECommerceBackend.Domain.Entities.Concrete;
using ECommerceBackend.Persistence.Contexts;

namespace ECommerceBackend.Persistence.Repositories;

public class ProductReadRepository : ReadRepository<Product>, IProductReadRepository
{
    public ProductReadRepository(ECommerceBackendDbContext context) : base(context)
    {
    }
}