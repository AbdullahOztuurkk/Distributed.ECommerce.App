using Clicco.AuthAPI.Data.Context;
using Clicco.AuthAPI.Data.Repositories;
using Clicco.AuthServiceAPI.Data.Contracts;
using Clicco.AuthServiceAPI.Models;

namespace Clicco.AuthServiceAPI.Data.Repositories
{
    public class ResetCodeRepository : GenericRepository<ResetCode, AuthContext>, IResetCodeRepository
    {
    }
}
