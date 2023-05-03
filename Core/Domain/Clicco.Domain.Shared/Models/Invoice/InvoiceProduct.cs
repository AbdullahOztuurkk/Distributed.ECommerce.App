using static Clicco.Domain.Shared.Global;

namespace Clicco.Domain.Shared.Models.Invoice
{
    public class InvoiceProduct
    {
        [DisplayElement("#PRODUCT_NAME#")]
        public string Name { get; set; }

        [DisplayElement("#PRODUCT_CODE#")]
        public string Code { get; set; }

        [DisplayElement("#PRODUCT_DESCRIPTION#")]
        public string Description { get; set; }

        [DisplayElement("#PRODUCT_QUANTITY#")]
        public int Quantity { get; set; }

        [DisplayElement("#PRODUCT_UNIT_PRICE#")]
        public int UnitPrice { get; set; }

        [DisplayElement("#PRODUCT_SLUG#")]
        public string SlugUrl { get; set; }
    }
}
