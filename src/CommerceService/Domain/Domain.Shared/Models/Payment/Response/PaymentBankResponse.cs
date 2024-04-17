namespace Clicco.Domain.Shared.Models.Payment
{
    public class PaymentBankResponse
    {
        public int TransactionId { get; set; }
        public string To { get; set; }
        public string FullName { get; set; }
        public string OrderNumber { get; set; }
        public string PaymentMethod { get; set; }
        public int TotalAmount { get; set; }
        public string ProductName { get; set; }
        public string ErrorMessage { get; set; }
    }
}
