using ECommerceBackend.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceBackend.Domain.Entities.Concrete
{
    public class Menu : BaseEntity
    {
        public string Name { get; set; }

        public ICollection<Endpoint> Endpoints { get; set; }
    }
}
