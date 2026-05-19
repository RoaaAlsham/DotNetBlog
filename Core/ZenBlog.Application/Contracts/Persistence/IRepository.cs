

using System.Linq.Expressions;
using ZenBlog.Domain.Entities.Common;

namespace ZenBlog.Application.Contracts.Persistence
{
    public interface IRepository<TEntity> where TEntity : BaseEntity
    {
        Task<TEntity?> GetByIdAsync(Guid id, CancellationToken ct);

        IQueryable<TEntity> GetQuery();
        Task<TEntity?> GetSingleAsync(Expression<Func<TEntity, bool>> filter, CancellationToken ct);
        Task<List<TEntity>> GetAllAsync(CancellationToken ct);

        Task CreateAsync(TEntity entity);
        Task UpdateAsync(TEntity entity);

        Task DeleteAsync(TEntity entity);

        Task<List<TEntity>> GetAllWithIncludesAsync(
    CancellationToken ct = default,
    params Expression<Func<TEntity, object>>[] includes);

        Task<List<TEntity>> GetAllWithIncludesAsync(
    Expression<Func<TEntity, bool>> filter,
    CancellationToken ct = default,
    params Expression<Func<TEntity, object>>[] includes);

        Task<TEntity?> GetSingleWithIncludesAsync(
    Expression<Func<TEntity, bool>> filter,
    CancellationToken ct = default,
    params Expression<Func<TEntity, object>>[] includes);
    }


}
