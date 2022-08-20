using ECommerceBackend.Application.Abstractions.Storage;
using ECommerceBackend.Application.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using static System.Net.Mime.MediaTypeNames;

namespace ECommerceBackend.Application.Features.Commands.ProductImageFile.RemoveProductImage;

public class RemoveProductImageCommandHandler : IRequestHandler<RemoveProductImageCommandRequest, RemoveProductImageCommandResponse>
{

    private readonly IProductReadRepository _productReadRepository;
    private readonly IProductWriteRepository _productWriteRepository;
    private readonly IStorageService _storageService;
    private readonly IFileWriteRepository _fileWriteRepository;

    public RemoveProductImageCommandHandler(IProductReadRepository productReadRepository, IProductWriteRepository productWriteRepository, IFileWriteRepository fileWriteRepository, IStorageService storageService)
    {
        _productReadRepository = productReadRepository;
        _productWriteRepository = productWriteRepository;
        _fileWriteRepository = fileWriteRepository;
        _storageService = storageService;
    }

    public async Task<RemoveProductImageCommandResponse> Handle(RemoveProductImageCommandRequest request, CancellationToken cancellationToken)
    {
        var product = await _productReadRepository.Table.Include(p => p.Images).FirstOrDefaultAsync(p => p.Id == Guid.Parse(request.Id), cancellationToken: cancellationToken);
        var image = product?.Images.FirstOrDefault(p => p.Id == Guid.Parse(request.ImageId));

        await _storageService.DeleteAsync(image!.Path, image.FileName);
        //todo async oldugu icin imagei bulamıyor hata veriyor  


        await _fileWriteRepository.Remove(request.Id);
        product?.Images.Remove(image!);
        await _productWriteRepository.SaveAsync();
        return new();
    }
}