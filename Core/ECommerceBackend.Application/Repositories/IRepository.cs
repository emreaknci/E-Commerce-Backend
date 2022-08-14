using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECommerceBackend.Domain.Entities.Common;
using Microsoft.EntityFrameworkCore;

namespace ECommerceBackend.Application.Repositories
{
    public interface IRepository<TEntity> where TEntity : BaseEntity
    {
        DbSet<TEntity> Table { get; }
    }
}
