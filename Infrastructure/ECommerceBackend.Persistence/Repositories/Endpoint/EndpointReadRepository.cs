using ECommerceBackend.Application.Repositories.Endpoint;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECommerceBackend.Persistence.Contexts;

namespace ECommerceBackend.Persistence.Repositories.Endpoint
{
    public class EndpointReadRepository : ReadRepository<Domain.Entities.Concrete.Endpoint>, IEndpointReadRepository
    {
        public EndpointReadRepository(ECommerceBackendDbContext context) : base(context)
        {
        }
    }
}
