using ECommerceBackend.Application.Repositories.Menu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECommerceBackend.Persistence.Contexts;

namespace ECommerceBackend.Persistence.Repositories.Menu
{
    public class MenuReadRepository : ReadRepository<Domain.Entities.Concrete.Menu>, IMenuReadRepository
    {
        public MenuReadRepository(ECommerceBackendDbContext context) : base(context)
        {
        }
    }
}
