using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace ECommerceBackend.Application.Features.Commands.CreateProduct
{
    public class CreateProductCommandRequest:IRequest<CreateProductCommandResponse>
    {
        public string Name { get; set; } = null!;
        public int UnitInStock { get; set; }
        public decimal Price { get; set; }
    }
}
