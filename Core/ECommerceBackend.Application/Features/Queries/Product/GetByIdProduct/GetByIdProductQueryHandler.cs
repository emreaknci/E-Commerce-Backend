using ECommerceBackend.Application.Repositories;
using MediatR;

namespace ECommerceBackend.Application.Features.Queries.Product.GetByIdProduct;

public class GetByIdProductQueryHandler : IRequestHandler<GetByIdProductQueryRequest, GetByIdProductQueryResponse>
{
    private readonly IProductReadRepository _productReadRepository;

    public GetByIdProductQueryHandler(IProductReadRepository productReadRepository)
    {
        _productReadRepository = productReadRepository;
    }

    public async Task<GetByIdProductQueryResponse> Handle(GetByIdProductQueryRequest request, CancellationToken cancellationToken)
    {
        var product = await _productReadRepository.GetByIdAsync(request.Id, false);
        return new()
        {
          Name = product.Name,
          Price = product.Price,
          UnitInStock = product.UnitInStock
        };
    }
}