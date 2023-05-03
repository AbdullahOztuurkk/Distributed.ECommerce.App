using static Clicco.Domain.Shared.Global;

namespace Clicco.Domain.Shared.Models.Invoice
{
    public class InvoiceCoupon
    {
        [DisplayElement("#COUPON_NAME#")]
        public string Name { get; set; }

        [DisplayElement("#COUPON_DESCRIPTION#")]
        public string Description { get; set; }

        [DisplayElement("#COUPON_TYPE#")]
        public string Type { get; set; }
        public int? TypeId { get; set; }

        [DisplayElement("#COUPON_EXPIRATION_DATE#")]
        public DateTime ExpirationDate { get; set; }
        public string DiscountType { get; set; }

        [DisplayElement("#COUPON_DISCOUNT_AMOUNT#")]
        public int DiscountAmount { get; set; }
    }
}
