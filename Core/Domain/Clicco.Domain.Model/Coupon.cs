using Clicco.Domain.Core;

namespace Clicco.Domain.Model
{
    public class Coupon: BaseEntity, ISoftDeletable
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public CouponType Type { get; set; }
        public int? TypeId { get; set; }
        public DateTime ExpirationDate { get; set; }
        public DiscountType DiscountType { get; set; }
        public decimal DiscountAmount { get; set; }
        public bool IsActive { get; set; } = true;
        public bool IsDeleted { get; set; } = false;

        //Relationship
        public Transaction Transaction { get; set; }
    }}