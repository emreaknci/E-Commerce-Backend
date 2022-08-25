using ECommerceBackend.Application.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace ECommerceBackend.Application.Features.Commands.Product.UpdateProduct;

public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommandRequest, UpdateProductCommandResponse>
{
    private readonly IProductReadRepository _productReadRepository;
    private readonly IProductWriteRepository _productWriteRepository;
    private readonly ILogger<UpdateProductCommandHandler> _logger;
    public UpdateProductCommandHandler(IProductReadRepository productReadRepository, IProductWriteRepository productWriteRepository, ILogger<UpdateProductCommandHandler> logger)
    {
        _productReadRepository = productReadRepository;
        _productWriteRepository = productWriteRepository;
        _logger = logger;
    }

    public async Task<UpdateProductCommandResponse> Handle(UpdateProductCommandRequest request, CancellationToken cancellationToken)
    {
        var product = await _productReadRepository.GetByIdAsync(request.Id);
        product.UnitInStock = request.UnitInStock;
        product.Name = request.Name;
        product.Price = request.Price;
        await _productWriteRepository.SaveAsync();
        _logger.LogInformation($"{product.Id}'li ürün bilgileri güncellendi...");
        return new();
    }
}