using ECommerceBackend.Domain.Entities.Common;

namespace ECommerceBackend.Domain.Entities.Concrete;

public class CompletedOrder : BaseEntity
{
    public Guid OrderId { get; set; }

    public Order? Order { get; set; }
}