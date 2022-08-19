using ECommerceBackend.Application.Repositories.File;
using ECommerceBackend.Persistence.Contexts;

namespace ECommerceBackend.Persistence.Repositories.File.InvoiceFile;

public class InvoiceFileWriteRepository : WriteRepository<Domain.Entities.Concrete.InvoiceFile>, IInvoiceFileWriteRepository
{
    public InvoiceFileWriteRepository(ECommerceBackendDbContext context) : base(context)
    {
    }
}