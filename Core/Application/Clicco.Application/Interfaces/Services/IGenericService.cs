namespace Clicco.Application.Interfaces.Services
{
    public interface IGenericService<TEntity>
    {
        void CheckSelfId(int entityId, string errorMessage);
    }
}
