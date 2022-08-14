using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceBackend.Persistence
{
    static class Configuration
    {
        public static string ConnectionString()
        {
            ConfigurationManager configurationManager = new();
            configurationManager.SetBasePath(Path.Combine(Directory.GetCurrentDirectory(),
                "../../Presantation/ECommerceBackend.API"));
            configurationManager.AddJsonFile("appsettings.json");
            return configurationManager.GetConnectionString("PostgreSQL");
        }
    }
}
