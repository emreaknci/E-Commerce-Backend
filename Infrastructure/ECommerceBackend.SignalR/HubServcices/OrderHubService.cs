using ECommerceBackend.Application.Abstractions.Hubs;
using ECommerceBackend.SignalR.Hubs;
using Microsoft.AspNetCore.SignalR;

namespace ECommerceBackend.SignalR.HubServcices;

public class OrderHubService : IOrderHubService
{
    private readonly IHubContext<OrderHub> _hubContext;

    public OrderHubService(IHubContext<OrderHub> hubContext)
    {
        _hubContext = hubContext;
    }

    public async Task OrderCreatedMessageAsync(string message) => await _hubContext.Clients.All.SendAsync(ReceiveFunctionNames.OrderCreatedMessage, message);

}