namespace Clicco.Domain.Shared.Models.Invoice
{
    public class InvoiceProduct
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }
        public int UnitPrice { get; set; }
        public string SlugUrl { get; set; }
    }
}
