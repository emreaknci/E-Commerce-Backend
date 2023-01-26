using ECommerceBackend.Application.Repositories.Menu;
using ECommerceBackend.Persistence.Contexts;

namespace ECommerceBackend.Persistence.Repositories.Menu;

public class MenuWriteRepository : WriteRepository<Domain.Entities.Concrete.Menu>, IMenuWriteRepository
{
    public MenuWriteRepository(ECommerceBackendDbContext context) : base(context)
    {
    }
}