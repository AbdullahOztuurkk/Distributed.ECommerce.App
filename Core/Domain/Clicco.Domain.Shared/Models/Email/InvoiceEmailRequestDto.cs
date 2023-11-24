using Clicco.Domain.Shared.Models.Invoice;

namespace Clicco.Domain.Shared.Models.Email
{
    public class InvoiceEmailRequestDto : BaseEmailRequest
    {
        public int Id { get; set; }
        public InvoiceTransaction Transaction { get; set; }
        public InvoiceProduct Product { get; set; }
        public InvoiceVendor Vendor { get; set; }
        public InvoiceAddress Address { get; set; }
        public InvoiceCoupon Coupon { get; set; }
    }
}
