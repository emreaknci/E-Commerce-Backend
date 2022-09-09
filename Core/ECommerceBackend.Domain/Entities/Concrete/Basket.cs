using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECommerceBackend.Domain.Entities.Common;
using ECommerceBackend.Domain.Entities.Identity;

namespace ECommerceBackend.Domain.Entities.Concrete
{
    public class Basket:BaseEntity
    {
        public string UserId { get; set; }

        public AppUser User { get; set; } = null!;
        public Order Order { get; set; } = null!;
        public ICollection<BasketItem> BasketItems { get; set; } = null!;
    }
}
