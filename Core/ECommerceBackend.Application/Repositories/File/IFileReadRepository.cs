using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using File = ECommerceBackend.Domain.Entities.Concrete.File;

namespace ECommerceBackend.Application.Repositories
{
    public interface IFileReadRepository : IReadRepository<Domain.Entities.Concrete.File>
    {
    }
}
