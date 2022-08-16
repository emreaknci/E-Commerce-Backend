using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceBackend.Application.ViewModels.Products
{
    public class VM_Create_Product
    {
        public string Name { get; set; } = null!;
        public int UnitInStock { get; set; }
        public decimal Price { get; set; }
    }
}
