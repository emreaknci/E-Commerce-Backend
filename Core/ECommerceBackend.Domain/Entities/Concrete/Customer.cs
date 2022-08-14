using ECommerceBackend.Domain.Entities.Common;

namespace ECommerceBackend.Domain.Entities.Concrete;

public class Customer : BaseEntity
{
    public string FullName { get; set; } = null!;
    public ICollection<Order> Orders { get; set; } = null!;

}