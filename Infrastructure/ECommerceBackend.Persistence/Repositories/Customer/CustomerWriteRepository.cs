using ECommerceBackend.Application.Repositories;
using ECommerceBackend.Domain.Entities.Concrete;
using ECommerceBackend.Persistence.Contexts;

namespace ECommerceBackend.Persistence.Repositories;

public class CustomerWriteRepository : WriteRepository<Customer>, ICustomerWriteRepository
{
    public CustomerWriteRepository(ECommerceBackendDbContext context) : base(context)
    {
    }
}