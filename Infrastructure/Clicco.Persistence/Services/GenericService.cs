using Clicco.Application.Interfaces.Services;
using Clicco.Domain.Core;
using Clicco.Domain.Core.Exceptions;
using Clicco.Domain.Model.Exceptions;

namespace Clicco.Persistence.Services
{
    public abstract class GenericService<TEntity> : IGenericService<TEntity> where TEntity : BaseEntity, new()
    {
        public abstract Task CheckSelfId(int entityId, CustomError err = null);

        public virtual void ThrowExceptionIfNull(object value, CustomError err)
        {
            if (value == null) throw new CustomException(err);
        }
    }
}
