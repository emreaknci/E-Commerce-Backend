using ECommerceBackend.Domain.Entities.Concrete;
using Microsoft.AspNetCore.Identity;

namespace ECommerceBackend.Domain.Entities.Identity;

public class AppRole : IdentityRole<string>
{
    public ICollection<Endpoint> Endpoints { get; set; }
}