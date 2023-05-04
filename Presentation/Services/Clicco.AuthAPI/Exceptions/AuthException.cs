namespace Clicco.AuthServiceAPI.Exceptions
{
    public class AuthException : Exception
    {
        public AuthException(string errorMessage) : base(errorMessage)
        {
            
        }
    }
}
