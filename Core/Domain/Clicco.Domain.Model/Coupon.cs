using Clicco.Domain.Core;

namespace Clicco.Domain.Model
{
    public class Coupon: BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public CouponType Type { get; set; }
        public int? TypeId { get; set; }
        public DateTime ExpirationDate { get; set; }
        public DiscountType DiscountType { get; set; }
        public int DiscountAmount { get; set; }
    }}