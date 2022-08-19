using ECommerceBackend.Application.Repositories;
using ECommerceBackend.Persistence.Contexts;
using File = ECommerceBackend.Domain.Entities.Concrete.File;

namespace ECommerceBackend.Persistence.Repositories;

public class FileWriteRepository:WriteRepository<Domain.Entities.Concrete.File>,IFileWriteRepository
{
    public FileWriteRepository(ECommerceBackendDbContext context) : base(context)
    {
    }
}