using static Clicco.Domain.Shared.Global;

namespace Clicco.Domain.Shared.Models.Invoice
{
    public class InvoiceVendor
    {
        [DisplayElement("#VENDOR_NAME#")]
        public string Name { get; set; }

        [DisplayElement("#VENDOR_EMAIL#")]
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Region { get; set; }

        [DisplayElement("#VENDOR_ADDRESS#")]
        public string Address { get; set; }
    }
}
