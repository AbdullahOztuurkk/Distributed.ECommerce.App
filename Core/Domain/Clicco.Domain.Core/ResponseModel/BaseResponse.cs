namespace Clicco.Domain.Core.ResponseModel
{
    public class BaseResponse
    {
        public bool IsSuccess { get; private set; }
        public string ErrorMessage { get; private set; }
        private BaseResponse(bool IsSuccess, string message)
        {
            this.IsSuccess = IsSuccess;
            ErrorMessage = message;
        }
        public class ErrorResponse : BaseResponse
        {
            public ErrorResponse(string Message) : base(false, Message) { }
        }

        public class SuccessResponse : BaseResponse
        {
            public SuccessResponse(string Message) : base(true, Message) { }
        }
    }
}
