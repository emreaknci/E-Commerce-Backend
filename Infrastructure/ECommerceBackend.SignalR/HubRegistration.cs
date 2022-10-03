using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECommerceBackend.SignalR.Hubs;
using Microsoft.AspNetCore.Builder;

namespace ECommerceBackend.SignalR
{
    public static class HubRegistration
    {
        public static void MapHubs(this WebApplication application)
        {
            application.MapHub<ProductHub>("/products-hub");
            application.MapHub<OrderHub>("/orders-hub");
        }
    }
}
