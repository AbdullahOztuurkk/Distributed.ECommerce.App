using static Clicco.Domain.Core.Exceptions.Errors;

namespace Clicco.Domain.Core.Exceptions
{
    public class CustomException : Exception
    {
        public CustomException(Error customError)
        {
            CustomError = customError;
        }

        public Error CustomError { get; set; }
    }
}
