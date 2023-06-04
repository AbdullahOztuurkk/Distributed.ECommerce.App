using Clicco.Application.Interfaces.Repositories;
using Clicco.Domain.Core;
using Clicco.Domain.Shared;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Clicco.Infrastructure.Repositories
{
    public class GenericRepository<TEntity, TContext> : IGenericRepository<TEntity>
        where TEntity : BaseEntity
        where TContext : DbContext, new()
    {
        private readonly TContext context;
        private DbSet<TEntity> Table => context.Set<TEntity>();
        public IQueryable<TEntity> Query => Table.AsQueryable();
        public GenericRepository()
        {
            context = new TContext();
        }
        public virtual async Task<TEntity> AddAsync(TEntity entity)
        {
            await Table.AddAsync(entity);
            return entity;
        }

        public virtual TEntity Delete(TEntity entity)
        {
            Table.Remove(entity);
            return entity;
        }

        public virtual async Task<List<TEntity>> Get(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, params Expression<Func<TEntity, object>>[] includes)
        {
            IQueryable<TEntity> query = Query;

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
            return await Table.ToListAsync();
        }

        public virtual async Task<TEntity> GetById(int id)
        {
            return await Table.FindAsync(id);
        }

        public virtual async Task<TEntity> GetByIdAsync(int id, params Expression<Func<TEntity, object>>[] includes)
        {
            IQueryable<TEntity> query = Query;

            foreach (Expression<Func<TEntity, object>> include in includes)
            {
                query = query.Include(include);
            }

            return await query.FirstOrDefaultAsync(x => x.Id == id);
        }

        public virtual async Task<TEntity> GetSingleAsync(Expression<Func<TEntity, bool>> expression, params Expression<Func<TEntity, object>>[] includes)
        {
            IQueryable<TEntity> query = Query;

            foreach (Expression<Func<TEntity, object>> include in includes)
            {
                query = query.Include(include);
            }

            return await query.SingleOrDefaultAsync(expression);
        }

        public async Task<int> SaveChangesAsync()
        {
            return await context.SaveChangesAsync();
        }

        public virtual TEntity Update(TEntity entity)
        {
            Table.Update(entity);
            return entity;
        }

        public async Task<List<TEntity>> PaginateAsync(Expression<Func<TEntity, bool>> filter, Global.PaginationFilter paginationFilter, params Expression<Func<TEntity, object>>[] includes)
        {
            IQueryable<TEntity> query = Query;

            foreach (Expression<Func<TEntity, object>> include in includes)
            {
                query = query.Include(include);
            }

            if (filter != null)
            {
                query = query.Where(filter);
            }

            query = query
                .Skip((paginationFilter.PageNumber - 1) * paginationFilter.PageSize)
                .Take(paginationFilter.PageSize);

            return await query.ToListAsync();
        }

        public async Task<List<TEntity>> PaginateAsync(Global.PaginationFilter paginationFilter, params Expression<Func<TEntity, object>>[] includes)
        {
            return await PaginateAsync(null, paginationFilter, includes);
        }
    }
}
