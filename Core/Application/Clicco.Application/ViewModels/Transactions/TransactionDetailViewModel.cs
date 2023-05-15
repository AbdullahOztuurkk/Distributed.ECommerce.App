using Clicco.Domain.Core;

namespace Clicco.Application.ViewModels
{
    public class TransactionDetailViewModel
    {
        public string Code { get; set; }
        public decimal TotalAmount { get; set; }
        public string Dealer { get; set; }
        public DateTime DeliveryDate { get; set; }
        public DateTime CreatedDate { get; set; }
        public string TransactionStatus { get; set; }
        public AddressViewModel Address { get; set; }
        public CouponViewModel Coupon { get; set; }
        public ProductViewModel Product { get; set; }

    }
}
