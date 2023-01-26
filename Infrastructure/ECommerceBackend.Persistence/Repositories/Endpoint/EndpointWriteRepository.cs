using ECommerceBackend.Application.Repositories.Endpoint;
using ECommerceBackend.Persistence.Contexts;

namespace ECommerceBackend.Persistence.Repositories.Endpoint;

public class EndpointWriteRepository : WriteRepository<Domain.Entities.Concrete.Endpoint>, IEndpointWriteRepository
{
    public EndpointWriteRepository(ECommerceBackendDbContext context) : base(context)
    {
    }
}