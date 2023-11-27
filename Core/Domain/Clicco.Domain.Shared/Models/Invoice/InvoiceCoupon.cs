using System.ComponentModel;

namespace Clicco.Domain.Shared.Models.Invoice
{
    public class InvoiceCoupon
    {
        [Description("#COUPON_NAME#")]
        public string Name { get; set; }

        [Description("#COUPON_DESCRIPTION#")]
        public string Description { get; set; }

        [Description("#COUPON_TYPE#")]
        public string Type { get; set; }
        public int? TypeId { get; set; }

        [Description("#COUPON_EXPIRATION_DATE#")]
        public DateTime ExpirationDate { get; set; }
        public string DiscountType { get; set; }

        [Description("#COUPON_DISCOUNT_AMOUNT#")]
        public int DiscountAmount { get; set; }
    }
}
