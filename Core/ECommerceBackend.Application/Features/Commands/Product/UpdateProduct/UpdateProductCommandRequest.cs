using MediatR;

namespace ECommerceBackend.Application.Features.Commands.Product.UpdateProduct;

public class UpdateProductCommandRequest : IRequest<UpdateProductCommandResponse>
{
    public string Id { get; set; }
    public string Name { get; set; } = null!;
    public int UnitInStock { get; set; }
    public decimal Price { get; set; }
}