namespace Clicco.Domain.Core.ResponseModel
{
    public class BaseResponse<T> where T : class, new()
    {
        public T? Data { get; set; }
        public bool IsSuccess { get; set; }
        public int StatusCode { get; set; }
        public string[]? Errors { get; set; }
        public string Message { get; set; }

        protected BaseResponse(T data, bool isSuccess, string[] errors, int statusCode = 500)
        {
            Data = data;
            IsSuccess = isSuccess;
            StatusCode = statusCode;
            Errors = errors;
            Message = string.Empty;
        }

        protected BaseResponse(T data, bool isSuccess, string message, int statusCode = 500) : this(data, isSuccess, Array.Empty<string>(), statusCode)
        {
            Message = message;
        }
    }

    public class ErrorResponse<T> : BaseResponse<T> where T : class, new()
    {
        public ErrorResponse(string message) : base(null, false, new string[] { message }, 400) { }
        public ErrorResponse(string[] messages) : base(null, false, messages, 400) { }
    }

    public class SuccessResponse<T> : BaseResponse<T> where T : class, new()
    {
        public SuccessResponse(string message) : base(null, true, new string[] { message }, 200) { }
        public SuccessResponse(T Data) : base(Data, true, Array.Empty<string>(), 200) { }
    }
}
