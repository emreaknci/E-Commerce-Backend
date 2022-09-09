using ECommerceBackend.Application.Repositories;
using ECommerceBackend.Domain.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECommerceBackend.Persistence.Contexts;

namespace ECommerceBackend.Persistence.Repositories
{
    public class BasketReadRepository : ReadRepository<Basket>, IBasketReadRepository
    {
        public BasketReadRepository(ECommerceBackendDbContext context) : base(context)
        {
        }
    }
}
