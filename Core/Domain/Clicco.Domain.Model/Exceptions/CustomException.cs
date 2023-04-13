using Clicco.Domain.Model.Exceptions;

namespace Clicco.Domain.Core.Exceptions
{
    public class CustomException : Exception
    {
        public CustomException(CustomError customError)
        {
            CustomError = customError;
        }

        public CustomError CustomError { get; set; }
    }
}
