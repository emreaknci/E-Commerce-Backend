using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECommerceBackend.Application.Repositories;
using ECommerceBackend.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using File = ECommerceBackend.Domain.Entities.Concrete.File;

namespace ECommerceBackend.Persistence.Repositories
{
    public class FileReadRepository:ReadRepository<Domain.Entities.Concrete.File>,IFileReadRepository
    {
        public FileReadRepository(ECommerceBackendDbContext context) : base(context)
        {
        }
    }
}
