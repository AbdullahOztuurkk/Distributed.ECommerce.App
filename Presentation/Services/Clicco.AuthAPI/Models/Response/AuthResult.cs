namespace Clicco.AuthServiceAPI.Models.Response
{
    public class AuthResult
    {
        public bool IsSuccess { get; set; }
        public int StatusCode { get; set; }
        public string Message { get; set; } = string.Empty;

        public AuthResult(bool IsSuccess, int StatusCode, string Message)
        {
            this.IsSuccess = IsSuccess;
            this.StatusCode = StatusCode;
            this.Message = Message;
        }
    }

    public class SuccessAuthResult : AuthResult
    {
        public SuccessAuthResult(string Message) : base(true, 200, Message)
        {

        }
        public SuccessAuthResult() : base(true, 200, string.Empty)
        {

        }
    }

    public class FailedAuthResult : AuthResult
    {
        public FailedAuthResult(string Message) : base(false, 400, Message)
        {

        }
    }
}
