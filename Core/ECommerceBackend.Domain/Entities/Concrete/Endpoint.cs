using ECommerceBackend.Domain.Entities.Common;
using ECommerceBackend.Domain.Entities.Identity;

namespace ECommerceBackend.Domain.Entities.Concrete;

public class Endpoint : BaseEntity
{
    public Endpoint()
    {
        Roles = new HashSet<AppRole>();
    }
    public string ActionType { get; set; }
    public string HttpType { get; set; }
    public string Definition { get; set; }
    public string Code { get; set; }

    public Menu Menu { get; set; }
    public ICollection<AppRole> Roles { get; set; }
}