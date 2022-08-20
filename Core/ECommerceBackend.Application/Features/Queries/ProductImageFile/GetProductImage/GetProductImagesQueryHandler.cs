using ECommerceBackend.Application.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace ECommerceBackend.Application.Features.Queries.ProductImageFile.GetProductImage;

public class GetProductImagesQueryHandler : IRequestHandler<GetProductImagesQueryRequest, List<GetProductImagesQueryResponse>>
{
    readonly IProductReadRepository _productReadRepository;
    readonly IConfiguration _configuration;

    public GetProductImagesQueryHandler(IProductReadRepository productReadRepository, IConfiguration configuration)
    {
        _productReadRepository = productReadRepository;
        _configuration = configuration;
    }

    public async Task<List<GetProductImagesQueryResponse>?> Handle(GetProductImagesQueryRequest request, CancellationToken cancellationToken)
    {
        var product = await _productReadRepository.Table.Include(p => p.Images)
            .FirstOrDefaultAsync(p => p.Id == Guid.Parse(request.Id), cancellationToken: cancellationToken);
        return product?.Images.Select(p => new GetProductImagesQueryResponse
        {
          Path = $"{_configuration["Storage:Azure:Url"]}/{p.Path}",
            FileName = p.FileName,
            Id = p.Id
        }).ToList();
    }
}