namespace Clicco.Application.ExternalModels.Email
{
    public class PaymentFailedEmailRequest
    {
        public string To { get; set; }
        public string FullName { get; set; }
        public string OrderNumber { get; set; }
        public string Amount { get; set; }
        public string PaymentMethod { get; set; }
        public string ProductName { get; set; }
        public string Error { get; set; }
    }
}
