namespace Clicco.AuthServiceAPI.Helpers.Contracts
{
    public interface IHashingHelper
    {
        bool VerifyPasswordHash(string password, byte[] userPasswordHash, byte[] userPasswordSalt);
        void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt);
    }
}
