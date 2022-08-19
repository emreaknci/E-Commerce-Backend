using ECommerceBackend.Application.Repositories.File;
using ECommerceBackend.Persistence.Contexts;

namespace ECommerceBackend.Persistence.Repositories.File.ProductImageFile;

public class ProductImageFileWriteRepository : WriteRepository<Domain.Entities.Concrete.ProductImageFile>,IProductImageFileWriteRepository
{
    public ProductImageFileWriteRepository(ECommerceBackendDbContext context) : base(context)
    {
    }
}