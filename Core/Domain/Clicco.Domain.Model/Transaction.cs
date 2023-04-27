using Clicco.Domain.Core;

namespace Clicco.Domain.Model
{
    public class Transaction : BaseEntity, ISoftDeletable
    {
        public string Code { get; set; }
        public int TotalAmount { get; set; }
        public int DiscountedAmount { get; set; }
        public string Dealer { get; set; }
        public DateTime DeliveryDate { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool IsDeleted { get; set; } = false;
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
