namespace Clicco.Application.ExternalModels.Payment.Request
{
    public  class PaymentRequest
    {
        public int BankId { get; set; }
        public int TotalAmount { get; set; }
        public int AddressId { get; set; }
        public int? CouponId { get; set; }
        public CardInformation CardInformation { get; set; }
        public int ProductId { get; set; } 
    }
}
