using Clicco.Application.Interfaces.Repositories;
using Clicco.Application.Interfaces.UnitOfWork;
using Clicco.Domain.Core;
using Clicco.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Clicco.Persistence.UnitOfWork
{
    public class UnitOfWork<TContext> : IUnitOfWork where TContext : DbContext, new()
    {
        private readonly Dictionary<Type,object> _repositories;
        private readonly TContext _context;

        public UnitOfWork(TContext context)
        {
            _repositories = new();
            _context = context;
        }

        public IRepository<TEntity> GetRepository<TEntity>() where TEntity : BaseEntity
        {
            if (_repositories.TryGetValue(typeof(IRepository<TEntity>), out var value))
            {
                if (value != null)
                    return value as IRepository<TEntity>;

                _repositories.Remove(typeof(IRepository<TEntity>));
            }

            GenericRepository<TEntity,TContext> genericRepository = new(_context);
            _repositories.Add(typeof(IRepository<TEntity>), genericRepository);
            return genericRepository;
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
