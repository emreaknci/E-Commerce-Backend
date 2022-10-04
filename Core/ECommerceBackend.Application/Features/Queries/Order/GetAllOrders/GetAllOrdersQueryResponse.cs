namespace ECommerceBackend.Application.Features.Queries.Order.GetAllOrders;

public class GetAllOrdersQueryResponse
{
    public int TotalOrderCount { get; set; }
    public object Orders { get; set; }
}