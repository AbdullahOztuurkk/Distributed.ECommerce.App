namespace Clicco.Domain.Shared.Models.Email
{
    public class PaymentFailedEmailRequestDto : BaseEmailRequest
    {
        public string FullName { get; set; }
        public string OrderNumber { get; set; }
        public string Amount { get; set; }
        public string PaymentMethod { get; set; }
        public string ProductName { get; set; }
        public string Error { get; set; }
    }
}
