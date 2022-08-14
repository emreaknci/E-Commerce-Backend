using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECommerceBackend.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace ECommerceBackend.Persistence
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<ECommerceBackendDbContext>
    {
        public ECommerceBackendDbContext CreateDbContext(string[] args)
        {

            DbContextOptionsBuilder<ECommerceBackendDbContext> dbContextOptionsBuilder = new();
            dbContextOptionsBuilder.UseNpgsql(Configuration.ConnectionString());
            return new ECommerceBackendDbContext(dbContextOptionsBuilder.Options);
        }
    }
}
