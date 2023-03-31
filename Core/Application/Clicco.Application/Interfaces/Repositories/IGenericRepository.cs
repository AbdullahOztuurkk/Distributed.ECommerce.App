using Clicco.Application.Interfaces.UnitOfWork;
using Clicco.Domain.Core;
using System.Linq.Expressions;

namespace Clicco.Application.Interfaces.Repositories
{
    public interface IGenericRepository<T> : IUnitOfWork where T : BaseEntity
    {
        Task<List<T>> GetAll();
        Task<List<T>> Get(Expression<Func<T, bool>> filter = null, params Expression<Func<T, object>>[] includes);
        Task<List<T>> Get(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, params Expression<Func<T, object>>[] includes);
        Task<T> GetById(int id);
        Task<T> GetByIdAsync(int id, params Expression<Func<T, object>>[] includes);
        Task<T> GetSingleAsync(Expression<Func<T, bool>> expression, params Expression<Func<T, object>>[] includes);
        Task<T> AddAsync(T entity);
        T Update(T entity);
        Task<T> DeleteAsync(T entity);
    }
}