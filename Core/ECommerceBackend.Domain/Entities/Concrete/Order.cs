﻿using ECommerceBackend.Domain.Entities.Common;

namespace ECommerceBackend.Domain.Entities.Concrete;

public class Order : BaseEntity
{
    //public Guid CustomerId { get; set; }
    public string Description { get; set; } = null!;
    public string Address { get; set; }
    public string? OrderCode { get; set; }

    //public virtual Customer Customer { get; set; } = null!; 
    public virtual Basket Basket { get; set; } = null!;

    //public virtual ICollection<Product> Products { get; set; } = null!;
    public CompletedOrder? CompletedOrder { get; set; }
}