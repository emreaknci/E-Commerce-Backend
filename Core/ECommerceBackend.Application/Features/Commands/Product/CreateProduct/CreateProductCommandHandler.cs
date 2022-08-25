
using ECommerceBackend.Application.Abstractions.Hubs;
using ECommerceBackend.Application.Repositories;
using MediatR;

namespace ECommerceBackend.Application.Features.Commands.Product.CreateProduct;

public class CreateProductCommandHandler:IRequestHandler<CreateProductCommandRequest,CreateProductCommandResponse>
{
    private readonly IProductWriteRepository _productWriteRepository;
    private readonly IProductHubService _productHubService;
    public CreateProductCommandHandler(IProductWriteRepository productWriteRepository, IProductHubService productHubService)
    {
        _productWriteRepository = productWriteRepository;
        _productHubService = productHubService;
    }

    public async Task<CreateProductCommandResponse> Handle(CreateProductCommandRequest request, CancellationToken cancellationToken)
    {
        await _productWriteRepository.AddAsync(new()
        {
            Name = request.Name,
            Price = request.Price,
            UnitInStock = request.UnitInStock
        });
        await _productWriteRepository.SaveAsync();
        await _productHubService.ProductAddedMessageAsync($"{request.Name} ürünü başarıyla eklendi.");
        return new();
    }
}