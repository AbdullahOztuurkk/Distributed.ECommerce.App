using Clicco.Application.Interfaces.Services;
using Clicco.Domain.Core;
using Clicco.Domain.Core.Exceptions;
using Clicco.Domain.Model.Exceptions;
using Clicco.Infrastructure.Context;
using System.Net;

namespace Clicco.Persistence.Services
{
    public class GenericService<TEntity> : IGenericService<TEntity> where TEntity : BaseEntity, new()
    {
        public virtual void CheckSelfId(int entityId, string errorMessage)
        {
            using var context = new CliccoContext();
            var result = context.Set<TEntity>().FirstOrDefault(x => x.Id == entityId);
            ThrowExceptionIfNull(result, CustomErrors.NotFoundArr[typeof(TEntity)]);
        }

        public virtual void ThrowExceptionIfNull(object value, CustomError err)
        {
            if (value == null) throw new CustomException(err);
        }
    }
}
