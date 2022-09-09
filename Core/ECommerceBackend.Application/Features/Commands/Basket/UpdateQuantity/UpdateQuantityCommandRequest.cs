using MediatR;

namespace ECommerceBackend.Application.Features.Commands.Basket.UpdateQuantity;

public class UpdateQuantityCommandRequest : IRequest<UpdateQuantityCommandResponse>
{
    public string BasketItemId { get; set; }
    public int Quantity { get; set; }
}