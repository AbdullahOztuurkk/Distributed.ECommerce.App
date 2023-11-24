using System.Net;

namespace Clicco.WebAPI.Models
{
    public class DynamicResponseModel
    {
        public string ErrorMessage { get; private set; }
        public string ErrorType { get; private set; }
        public DynamicResponseModel(string ErrorMessage, string ErrorType)
        {
            this.ErrorMessage = ErrorMessage;
            this.ErrorType = ErrorType;
        }

        public DynamicResponseModel(string ErrorMessage) : this(ErrorMessage, nameof(HttpStatusCode.BadRequest))
        {

        }

    }
}
