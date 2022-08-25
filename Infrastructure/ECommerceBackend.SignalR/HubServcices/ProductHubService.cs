using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECommerceBackend.Application.Abstractions.Hubs;
using ECommerceBackend.SignalR.Hubs;
using Microsoft.AspNetCore.SignalR;

namespace ECommerceBackend.SignalR.HubServcices
{
    public class ProductHubService: IProductHubService
    {
        readonly IHubContext<ProductHub> _hubContext;

        public ProductHubService(IHubContext<ProductHub> hubContext)
        {
            _hubContext = hubContext;
        }

        public async Task ProductAddedMessageAsync(string message)
        {
            await _hubContext.Clients.All.SendAsync(ReceiveFunctionNames.ProductAddedMessage, message);
        }
    }
}
