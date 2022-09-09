namespace ECommerceBackend.Application.Features.Queries.Basket.GetBasketItems;

public class GetBasketItemsQueryResponse
{
    public string BasketItemId { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public int Quantity { get; set; }
}