using ECommerceBackend.Application.Features.Commands.Product.CreateProduct;
using MediatR;

namespace ECommerceBackend.Application.Features.Commands.Product.AddRangeProduct;

public class AddRangeProductRequest:IRequest<AddRangeProductResponse>
{
    public List<CreateProductCommandRequest> Items { get; set; }
}