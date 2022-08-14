using ECommerceBackend.Domain.Entities.Common;
using System.Linq.Expressions;

namespace ECommerceBackend.Application.Repositories;

public interface IReadRepository<TEntity> : IRepository<TEntity> where TEntity : BaseEntity
{
    IQueryable<TEntity> GetAll(Expression<Func<TEntity, bool>>? filter=null, bool tracking = true);
    Task<TEntity?> GetSingleAsync(Expression<Func<TEntity, bool>> filter, bool tracking=true);
    Task<TEntity?> GetByIdAsync(string id, bool tracking = true);
}