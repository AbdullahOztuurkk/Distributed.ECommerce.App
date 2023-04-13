using Clicco.AuthAPI.Data.Context;
using Clicco.AuthAPI.Data.Contracts;
using Clicco.AuthAPI.Models;

namespace Clicco.AuthAPI.Data.Repositories
{
    public class UserRepository : GenericRepository<User, AuthContext>, IUserRepository
    {

    }
}
