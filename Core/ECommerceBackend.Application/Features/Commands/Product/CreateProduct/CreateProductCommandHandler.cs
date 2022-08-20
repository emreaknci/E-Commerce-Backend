﻿
using ECommerceBackend.Application.Repositories;
using MediatR;

namespace ECommerceBackend.Application.Features.Commands.Product.CreateProduct;

public class CreateProductCommandHandler:IRequestHandler<CreateProductCommandRequest,CreateProductCommandResponse>
{
    private readonly IProductWriteRepository _productWriteRepository;

    public CreateProductCommandHandler(IProductWriteRepository productWriteRepository)
    {
        _productWriteRepository = productWriteRepository;
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
        return new();
    }
}