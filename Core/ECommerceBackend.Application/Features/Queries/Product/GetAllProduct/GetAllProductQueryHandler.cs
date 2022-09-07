using ECommerceBackend.Application.Features.Commands.Product.UpdateProduct;
using ECommerceBackend.Application.Repositories;
using ECommerceBackend.Application.RequestParameters;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ECommerceBackend.Application.Features.Queries.Product.GetAllProduct;

public class GetAllProductQueryHandler : IRequestHandler<GetAllProductQueryRequest, GetAllProductQueryResponse>
{
    private readonly IProductReadRepository _productReadRepository;
    private readonly ILogger<GetAllProductQueryHandler> _logger;
    public GetAllProductQueryHandler(IProductReadRepository productReadRepository, ILogger<GetAllProductQueryHandler> logger)
    {
        _productReadRepository = productReadRepository;
        _logger = logger;
    }

    public async Task<GetAllProductQueryResponse> Handle(GetAllProductQueryRequest request, CancellationToken cancellationToken)
    {
        var totalProductCount = _productReadRepository.GetAll(false).Count();
        var products = _productReadRepository.GetAll(false).Skip(request.Page * request.Size).Take(request.Size)
            .Include(p => p.Images)
            .Select(p => new
            {
                p.Id,
                p.Name,
                p.UnitInStock,
                p.Price,
                p.CreatedDate,
                p.UpdatedDate,
                p.Images
            }).ToList();
        _logger.LogInformation("Tüm ürünler listelendi");
        return new()
        {
            Products = products,
            TotalProductCount = totalProductCount
        };
    }
}