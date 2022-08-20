using ECommerceBackend.Application.Abstractions.Storage;
using ECommerceBackend.Application.Repositories;
using ECommerceBackend.Application.Repositories.File;
using MediatR;

namespace ECommerceBackend.Application.Features.Commands.ProductImageFile.UploadProductImage;

public class UploadProductImageCommandHandler : IRequestHandler<UploadProductImageCommandRequest, UploadProductImageCommandResponse>
{
    readonly IStorageService _storageService;
    readonly IProductReadRepository _productReadRepository;
    readonly IProductImageFileWriteRepository _productImageFileWriteRepository;

    public UploadProductImageCommandHandler(IStorageService storageService, IProductReadRepository productReadRepository, IProductImageFileWriteRepository productImageFileWriteRepository)
    {
        _storageService = storageService;
        _productReadRepository = productReadRepository;
        _productImageFileWriteRepository = productImageFileWriteRepository;
    }

    public async Task<UploadProductImageCommandResponse> Handle(UploadProductImageCommandRequest request, CancellationToken cancellationToken)
    {

        var results = await _storageService.UploadAsync("photo-images", request.Files);
        var product = await _productReadRepository.GetByIdAsync(request.Id);

        await _productImageFileWriteRepository.AddRangeAsync(results.Select(r => new Domain.Entities.Concrete.ProductImageFile()
        {
            FileName = r.fileName,
            Path = r.pathOrContainerName,
            Storage = _storageService.StorageName,
            Products = new List<Domain.Entities.Concrete.Product>() { product }
        }).ToList()); ;

        await _productImageFileWriteRepository.SaveAsync();

        return new();
    }
}