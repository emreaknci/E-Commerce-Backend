using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECommerceBackend.Domain.Entities.Concrete;

namespace ECommerceBackend.Application.Repositories
{
    public interface ICompletedOrderReadRepository : IReadRepository<CompletedOrder>
    {
    }
}
