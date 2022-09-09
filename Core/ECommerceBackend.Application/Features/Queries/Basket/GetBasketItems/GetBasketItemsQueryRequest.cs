using MediatR;

namespace ECommerceBackend.Application.Features.Queries.Basket.GetBasketItems;

public class GetBasketItemsQueryRequest : IRequest<List<GetBasketItemsQueryResponse>>
{
}