using Domain.Common;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IAsyncRepository<TEntity> where TEntity : BaseEntity
    {
        IQueryable<TEntity> SelectAsync();

        Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> expression);

        Task<TEntity> SelectAsync(string id);

        Task<TEntity> AddAsync(TEntity entity, bool saveAutomatically = true);

        Task UpdateAsync(TEntity entity, bool saveAutomatically = true);

        Task DeleteAsync(TEntity entity, bool saveAutomatically = true);

        Task DeleteAsync(string id, bool saveAutomatically = true);

        Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> predicate);

        Task<bool> ExistsAsync(string id);
        Task SaveChangesAsync();
    }
}
