using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceBackend.Application.DTOs.Configurations
{
    public class Menu
    {
        public string Name { get; set; }
        public List<Action> Actions { get; set; } = new();
    }
}
