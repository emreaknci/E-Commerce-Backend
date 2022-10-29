using MediatR;

namespace ECommerceBackend.Application.Features.Commands.Order.CompleteOrder;

public class CompleteOrderCommandRequest : IRequest<CompleteOrderCommandResponse>
{
    public string Id { get; set; }
}