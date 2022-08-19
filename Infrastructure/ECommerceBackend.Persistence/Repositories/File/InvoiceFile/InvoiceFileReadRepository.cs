using ECommerceBackend.Application.Repositories.File;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECommerceBackend.Persistence.Contexts;

namespace ECommerceBackend.Persistence.Repositories.File.InvoiceFile
{
    public class InvoiceFileReadRepository : ReadRepository<Domain.Entities.Concrete.InvoiceFile>, IInvoiceFileReadRepository
    {
        public InvoiceFileReadRepository(ECommerceBackendDbContext context) : base(context)
        {
        }
    }
}
