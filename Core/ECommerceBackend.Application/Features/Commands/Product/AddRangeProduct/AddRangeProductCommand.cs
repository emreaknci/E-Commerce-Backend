using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using ECommerceBackend.Application.Abstractions.Hubs;
using ECommerceBackend.Application.Repositories;
using MediatR;

namespace ECommerceBackend.Application.Features.Commands.Product.AddRangeProduct
{
    public class AddRangeProductHandler : IRequestHandler<AddRangeProductRequest, AddRangeProductResponse>
    {
        private readonly IProductWriteRepository _productWriteRepository;

        public AddRangeProductHandler(IProductWriteRepository productWriteRepository)
        {
            _productWriteRepository = productWriteRepository;
        }

        public async Task<AddRangeProductResponse> Handle(AddRangeProductRequest request, CancellationToken cancellationToken)
        {
            List<Domain.Entities.Concrete.Product> products = new();
            foreach (var item in request.Items)
                products.Add(new() { Name = item.Name, Price = item.Price, UnitInStock = item.UnitInStock });

            await _productWriteRepository.AddRangeAsync(products);
            await _productWriteRepository.SaveAsync();
            return new() { };
        }
    }
}
