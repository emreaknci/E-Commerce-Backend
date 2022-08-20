using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECommerceBackend.Application.RequestParameters;
using MediatR;

namespace ECommerceBackend.Application.Features.Queries.GetAllProduct
{
    public class GetAllProductQueryRequest :IRequest<GetAllProductQueryResponse>
    {
        //public Pagination Pagination { get; set; }
        public int Page { get; set; } = 0;
        public int Size { get; set; } = 5;
    }
}
