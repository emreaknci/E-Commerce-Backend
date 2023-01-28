using ECommerceBackend.Application.Abstractions.Services;
using MediatR;

namespace ECommerceBackend.Application.Features.Commands.Product.UpdateStockQrCodeToProduct;

public class UpdateStockQrCodeToProductCommandHandler : IRequestHandler<UpdateStockQrCodeToProductCommandRequest, UpdateStockQrCodeToProductCommandResponse>
{
    readonly IProductService _productService;

    public UpdateStockQrCodeToProductCommandHandler(IProductService productService)
    {
        _productService = productService;
    }

    public async Task<UpdateStockQrCodeToProductCommandResponse> Handle(UpdateStockQrCodeToProductCommandRequest request, CancellationToken cancellationToken)
    {
        await _productService.StockUpdateToProductAsync(request.ProductId, request.Stock);
        return new();
    }
}