using Clicco.AuthAPI.Data.Contracts;
using Clicco.AuthAPI.Models.Common;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Clicco.AuthAPI.Data.Repositories
{
    public class GenericRepository<TEntity, TContext> : IGenericRepository<TEntity>
        where TEntity : BaseEntity
        where TContext : DbContext, new()
    {

        public virtual async Task<TEntity> AddAsync(TEntity entity)
        {
            using var context = new TContext();
            await context.Set<TEntity>().AddAsync(entity);
            return entity;
        }

        public virtual async Task<TEntity> DeleteAsync(TEntity entity)
        {
            using var context = new TContext();
            context.Entry(entity).State = EntityState.Deleted;
            return entity;
        }

        public virtual async Task<List<TEntity>> Get(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, params Expression<Func<TEntity, object>>[] includes)
        {
            using var context = new TContext();
            IQueryable<TEntity> query = context.Set<TEntity>();

            foreach (Expression<Func<TEntity, object>> include in includes)
            {
                query = query.Include(include);
            }

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (orderBy != null)
            {
                query = orderBy(query);
            }

            return await query.ToListAsync();
        }

        public virtual Task<List<TEntity>> Get(Expression<Func<TEntity, bool>> filter = null, params Expression<Func<TEntity, object>>[] includes)
        {
            return Get(filter, null, includes);
        }

        public virtual async Task<List<TEntity>> GetAll()
        {
            using var context = new TContext();
            return await context.Set<TEntity>().ToListAsync();
        }

        public virtual async Task<TEntity> GetById(int id)
        {
            using var context = new TContext();
            return await context.Set<TEntity>().FindAsync(id);
        }

        public virtual async Task<TEntity> GetByIdAsync(int id, params Expression<Func<TEntity, object>>[] includes)
        {
            using var context = new TContext();

            IQueryable<TEntity> query = context.Set<TEntity>();

            foreach (Expression<Func<TEntity, object>> include in includes)
            {
                query = query.Include(include);
            }

            return await query.FirstOrDefaultAsync(x => x.Id == id);
        }

        public virtual async Task<TEntity> GetSingleAsync(Expression<Func<TEntity, bool>> expression, params Expression<Func<TEntity, object>>[] includes)
        {
            using var context = new TContext();
            IQueryable<TEntity> query = context.Set<TEntity>();

            foreach (Expression<Func<TEntity, object>> include in includes)
            {
                query = query.Include(include);
            }

            return await query.SingleOrDefaultAsync();
        }

        public async Task<int> SaveChangesAsync()
        {
            using var context = new TContext();
            return await context.SaveChangesAsync();
        }

        public virtual TEntity Update(TEntity entity)
        {
            using var context = new TContext();
            context.Set<TEntity>().Update(entity);
            return entity;
        }
    }
}
