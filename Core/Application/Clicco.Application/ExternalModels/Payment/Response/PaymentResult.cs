namespace Clicco.Application.ExternalModels.Payment.Response
{
    public class PaymentResult
    {
        public bool IsSuccess { get; set; }
        public int StatusCode { get; set; }
        public string Message { get; set; } = string.Empty;

        public PaymentResult(bool IsSuccess, int StatusCode, string Message)
        {
            this.IsSuccess = IsSuccess;
            this.StatusCode = StatusCode;
            this.Message = Message;
        }
    }

    public class SuccessPaymentResult : PaymentResult
    {
        public SuccessPaymentResult(string Message) : base(true, 200, Message)
        {

        }
        public SuccessPaymentResult() : base(true, 200, string.Empty)
        {

        }
    }

    public class FailedPaymentResult : PaymentResult
    {
        public FailedPaymentResult(string Message) : base(false, 400, Message)
        {

        }
    }
}
