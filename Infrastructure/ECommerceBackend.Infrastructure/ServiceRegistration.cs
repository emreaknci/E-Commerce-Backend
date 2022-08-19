using ECommerceBackend.Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECommerceBackend.Application.Abstractions.Storage;
using ECommerceBackend.Infrastructure.Enums;
using ECommerceBackend.Infrastructure.Services.Storage;
using ECommerceBackend.Infrastructure.Services.Storage.Local;

namespace ECommerceBackend.Infrastructure
{
    public static class ServiceRegistration
    {
        public static void AddInfrastructureServices(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<IStorageService, StorageService>();
        }
        public static void AddStorage(this IServiceCollection serviceCollection,StorageType storageType) 
        {
            switch (storageType)
            {
                case StorageType.Local:
                    serviceCollection.AddScoped<IStorage, LocalStorage>();
                    break;
                case StorageType.Azure:
                    break;
                case StorageType.AWS:
                    break;
                default:
                    serviceCollection.AddScoped<IStorage, LocalStorage>();
                    break;
            }

        }
        public static void AddStorage<T>(this IServiceCollection serviceCollection) where T:class, IStorage
        {
            serviceCollection.AddScoped<IStorage, T>();

        }
    }
}
