using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECommerceBackend.Application.Abstractions.Hubs;
using ECommerceBackend.SignalR.HubServcices;
using Microsoft.Extensions.DependencyInjection;

namespace ECommerceBackend.SignalR
{
    public static class ServiceRegistration
    {
        public static void AddSignalRService(this IServiceCollection collection)
        {
            collection.AddTransient<IProductHubService, ProductHubService>();
            collection.AddTransient<IOrderHubService, OrderHubService>();
            collection.AddSignalR();
        }
    }
}
