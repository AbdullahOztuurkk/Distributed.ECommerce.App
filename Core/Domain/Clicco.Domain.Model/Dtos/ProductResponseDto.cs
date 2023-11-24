namespace Clicco.Domain.Model.Dtos
{
    public class ProductResponseDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }
        public int UnitPrice { get; set; }
        public string SlugUrl { get; set; }
        public string CategoryName { get; set; }
        public string VendorName { get; set; }
    }
}