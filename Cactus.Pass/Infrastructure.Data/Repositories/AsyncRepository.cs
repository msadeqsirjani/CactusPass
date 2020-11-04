using Domain.Common;
using Domain.Interfaces;
using Infrastructure.Data.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Infrastructure.Data.Repositories
{
    public class AsyncRepository<TEntity> : IAsyncRepository<TEntity> where TEntity : BaseEntity
    {
        protected readonly CactusPassDbContext Db;
        protected readonly DbSet<TEntity> DbSet;

        protected AsyncRepository(CactusPassDbContext db)
        {
            Db = db;
            DbSet = Db.Set<TEntity>();
        }

        public virtual IQueryable<TEntity> SelectAsync() => DbSet.AsNoTracking();

        public async Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> expression)
            => await SelectAsync().FirstOrDefaultAsync(expression);

        public virtual async Task<TEntity> SelectAsync(string id)
            => await FirstOrDefaultAsync(e => e.Id == id);

        public virtual async Task<TEntity> AddAsync(TEntity entity, bool saveAutomatically = true)
        {
            await DbSet.AddAsync(entity);

            if (saveAutomatically)
                await SaveChangesAsync();

            return entity;
        }

        public virtual async Task UpdateAsync(TEntity entity, bool saveAutomatically = true)
        {
            DbSet.Update(entity);

            if (saveAutomatically)
                await SaveChangesAsync();
        }

        public virtual async Task DeleteAsync(TEntity entity, bool saveAutomatically = true)
        {
            DbSet.Remove(entity);

            if (saveAutomatically)
                await SaveChangesAsync();
        }

        public virtual async Task DeleteAsync(string id, bool saveAutomatically = true)
        {
            var entity = await SelectAsync(id);

            await DeleteAsync(entity);
        }

        public virtual async Task SaveChangesAsync()
        {
            await Db.SaveChangesAsync();
        }

        public virtual async Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> predicate) =>
            await DbSet.AnyAsync(predicate);

        public virtual async Task<bool> ExistsAsync(string id) => await ExistsAsync(e => e.Id == id);
    }
}
