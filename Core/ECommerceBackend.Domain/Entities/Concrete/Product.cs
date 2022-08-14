using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECommerceBackend.Domain.Entities.Common;

namespace ECommerceBackend.Domain.Entities.Concrete
{
    public class Product : BaseEntity
    {
        public string Name { get; set; } = null!;
        public int UnitInStock { get; set; }
        public decimal Price { get; set; }
        public virtual ICollection<Order> Orders { get; set; } = null!;

    }
}
