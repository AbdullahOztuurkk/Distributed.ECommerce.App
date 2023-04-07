using Clicco.Application.Interfaces.Services;
using Clicco.Domain.Core;
using Clicco.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace Clicco.Persistence.Services
{
    public class GenericService<TEntity> : IGenericService<TEntity> where TEntity : BaseEntity, new()
    {
        public virtual void CheckSelfId(int entityId,string errorMessage)
        {
            using var context = new CliccoContext();
            var result = context.Set<TEntity>().FirstOrDefault(x => x.Id == entityId);
            ThrowExceptionIfNull(result, errorMessage);
        }

        public virtual void ThrowExceptionIfNull(object value,string errorMessage) 
        {
            if (value == null) throw new Exception(errorMessage) { HResult = (int)HttpStatusCode.BadRequest };
        }
    }
}
