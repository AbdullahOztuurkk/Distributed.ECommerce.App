using static Clicco.Domain.Shared.Global;

namespace Clicco.Domain.Shared.Models.Invoice
{
    public class InvoiceAddress
    {
        [DisplayElement("#ADDRESS_CITY#")]
        public string City { get; set; }

        [DisplayElement("#ADDRESS_STREET#")]
        public string Street { get; set; }

        [DisplayElement("#ADDRESS_STATE#")]
        public string State { get; set; }

        [DisplayElement("#ADDRESS_COUNTRY#")]
        public string Country { get; set; }

        [DisplayElement("#ADDRESS_ZIPCODE#")]
        public string ZipCode { get; set; }
    }
}
