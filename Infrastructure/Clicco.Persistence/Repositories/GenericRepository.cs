using Clicco.Application.Interfaces.Repositories;
using Clicco.Domain.Core;
using Clicco.Domain.Shared;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Clicco.Persistence.Repositories
{
    public class GenericRepository<TEntity, TContext> : IRepository<TEntity>
        where TEntity : BaseEntity
        where TContext : DbContext, new()
    {
        public readonly TContext context;
        public DbSet<TEntity> Table => context.Set<TEntity>();
        public IQueryable<TEntity> Query => Table.AsQueryable();
        public GenericRepository(TContext context)
        {
            this.context = context;
        }
        public async Task<TEntity> AddAsync(TEntity entity)
        {
            await Table.AddAsync(entity);
            return entity;
        }

        public TEntity Delete(TEntity entity)
        {
            Table.Remove(entity);
            return entity;
        }

        public async Task<List<TEntity>> GetManyAsync(Expression<Func<TEntity, bool>> filter = null, params Expression<Func<TEntity, object>>[] includes)
        {
            IQueryable<TEntity> query = Query;
            foreach (Expression<Func<TEntity, object>> include in includes)
            {
                query = query.Include(include);
            }
            return query.Where(filter).ToList();
        }

        public async Task<TEntity> GetByIdAsync(int id)
        {
            return await Table.FindAsync(id);
        }

        public async Task<TEntity> GetByIdAsync(int id, params Expression<Func<TEntity, object>>[] includes)
        {
            IQueryable<TEntity> query = Query;

            foreach (Expression<Func<TEntity, object>> include in includes)
            {
                query = query.Include(include);
            }

            return await query.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> expression, params Expression<Func<TEntity, object>>[] includes)
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

        public TEntity Update(TEntity entity)
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
