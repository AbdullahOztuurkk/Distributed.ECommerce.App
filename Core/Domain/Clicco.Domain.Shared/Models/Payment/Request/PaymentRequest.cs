using MediatR;

namespace Clicco.Domain.Shared.Models.Payment
{
    public class PaymentRequest : IRequest<PaymentResult>
    {
        public int BankId { get; set; }
        public int Quantity { get; set; }
        public int AddressId { get; set; }
        public int? CouponId { get; set; }
        public CardInformation CardInformation { get; set; }
        public int ProductId { get; set; }
    }
}
