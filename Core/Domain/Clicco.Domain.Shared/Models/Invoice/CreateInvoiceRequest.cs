namespace Clicco.Domain.Shared.Models.Invoice
{
    public class CreateInvoiceRequest
    {
        public InvoiceTransaction Transaction { get; set; }
        public InvoiceProduct Product { get; set; }
        public InvoiceVendor Vendor { get; set; }
        public InvoiceAddress Address { get; set; }
        public InvoiceCoupon Coupon { get; set; }
    }
}
