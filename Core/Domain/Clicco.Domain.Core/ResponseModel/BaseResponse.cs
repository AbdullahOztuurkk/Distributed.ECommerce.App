namespace Clicco.Domain.Core.ResponseModel
{
    public class BaseResponse
    {
        public bool IsSuccess { get; private set; }
        public int StatusCode { get; private set; }
        public List<string> ErrorMessage { get; private set; }
        protected BaseResponse(bool IsSuccess, List<string> message, int statusCode = 500)
        {
            this.IsSuccess = IsSuccess;
            ErrorMessage = message;
        }

    }
    public class ErrorResponse : BaseResponse
    {
        public ErrorResponse(string Message) : base(false, new List<string> { Message }, 400) { }
        public ErrorResponse(List<string> Messages) : base(false, Messages, 400) { }

    }

    public class SuccessResponse : BaseResponse
    {
        public SuccessResponse(string Message) : base(true, new List<string> { Message }, 200) { }
    }
}
