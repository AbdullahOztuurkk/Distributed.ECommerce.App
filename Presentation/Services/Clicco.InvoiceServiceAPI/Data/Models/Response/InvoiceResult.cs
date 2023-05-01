namespace Clicco.InvoiceServiceAPI.Data.Models
{
    public class InvoiceResult
    {
        public bool IsSuccess { get; set; }
        public int StatusCode { get; set; }
        public string Message { get; set; } = string.Empty;

        public InvoiceResult(bool IsSuccess, int StatusCode, string Message)
        {
            this.IsSuccess = IsSuccess;
            this.StatusCode = StatusCode;
            this.Message = Message;
        }
    }

    public class SuccessInvoiceResult : InvoiceResult
    {
        public SuccessInvoiceResult(string Message) : base(true, 200, Message)
        {

        }
        public SuccessInvoiceResult() : base(true, 200, string.Empty)
        {

        }
    }

    public class FailedInvoiceResult : InvoiceResult
    {
        public FailedInvoiceResult(string Message) : base(false, 400, Message)
        {

        }
    }
}
