using ECommerceBackend.Application.Repositories.File;
using ECommerceBackend.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceBackend.Persistence.Repositories.File.ProductImageFile
{
    public class ProductImageFileReadRepository : ReadRepository<Domain.Entities.Concrete.ProductImageFile>, IProductImageFileReadRepository
    {
        public ProductImageFileReadRepository(ECommerceBackendDbContext context) : base(context)
        {
        }
    }
}
