using MediatR;

namespace ECommerceBackend.Application.Features.Commands.Basket.RemoveBasketItem;

public class RemoveBasketItemCommandRequest : IRequest<RemoveBasketItemCommandResponse>
{
    public string BasketItemId { get; set; }
}