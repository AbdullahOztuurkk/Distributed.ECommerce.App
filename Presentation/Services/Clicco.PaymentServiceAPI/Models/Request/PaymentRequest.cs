namespace Clicco.PaymentServiceAPI.Models.Request
{
    public class PaymentRequest
    {
        public int BankId { get; set; }
        public CardInformation CardInformation { get; set; }
        public string DealerName { get; set; }
        public string TotalAmount { get; set; }
        public string ProductName { get; set; }
    }

    public class CardInformation
    {
        public string CardOwner { get; set; }
        public string CardNumber { get; set;}
        public string CardSecurityNumber { get; set;}
        public DateTime ExpirationDate { get; set;}
    }
}
