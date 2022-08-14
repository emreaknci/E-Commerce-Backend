using ECommerceBackend.Application.Repositories;
using ECommerceBackend.Domain.Entities.Common;
using ECommerceBackend.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace ECommerceBackend.Persistence.Repositories;

public class WriteRepository<TEntity> : IWriteRepository<TEntity> where TEntity : BaseEntity
{
    private readonly ECommerceBackendDbContext _context;

    public WriteRepository(ECommerceBackendDbContext context)
    {
        _context = context;
    }
    public DbSet<TEntity> Table => _context.Set<TEntity>();

    public async Task<bool> AddAsync(TEntity entity)
    {
        EntityEntry<TEntity> entityEntry = await Table.AddAsync(entity);
        return entityEntry.State == EntityState.Added;
    }
    public async Task<bool> AddRangeAsync(List<TEntity> entities)
    {
        await Table.AddRangeAsync(entities);
        return true;
    }

    public bool Remove(TEntity entity)
    {
        EntityEntry<TEntity> entityEntry = Table.Remove(entity);
        return entityEntry.State == EntityState.Deleted;
    }

    public async Task<bool> Remove(string id)
    {
        var entity = await Table.FirstOrDefaultAsync(p => p.Id == Guid.Parse(id));
        return entity != null && Remove(entity);
    }

    public bool RemoveRange(List<TEntity> entities)
    {
        Table.RemoveRange(entities);
        return true;
    }

    public bool Update(TEntity entity)
    {
        EntityEntry<TEntity> entityEntry = Table.Update(entity);
        return entityEntry.State == EntityState.Modified;
    }

    public async Task<int> SaveAsync()
        => await _context.SaveChangesAsync();
}