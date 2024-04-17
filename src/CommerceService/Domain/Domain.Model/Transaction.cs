using Clicco.Domain.Core;

namespace Clicco.Domain.Model
{
    public class Transaction : BaseEntity
    {
        public string Code { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal DiscountedAmount { get; set; }
        public string Dealer { get; set; }
        public DateTime DeliveryDate { get; set; }
        public TransactionStatus TransactionStatus { get; set; } = TransactionStatus.Pending;

        //Relationship
        public int UserId { get; set; }
        public Address Address { get; set; }
        public int AddressId { get; set; }
        public Coupon Coupon { get; set; }
        public int CouponId { get; set; }
        public TransactionDetail TransactionDetail { get; set; }
        public int TransactionDetailId { get; set; }
    }
}
