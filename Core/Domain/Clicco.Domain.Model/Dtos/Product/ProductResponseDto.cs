using Clicco.Domain.Core.ResponseModel;

namespace Clicco.Domain.Model.Dtos.Product
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
        public int CategoryId { get; set; }
        public int VendorId { get; set; }
        public string CategoryName { get; set; }
        public string VendorName { get; set; }

        public ProductResponseDto Map(Model.Product product)
        {
            this.Id = product.Id;
            this.Name = product.Name;
            this.Code = product.Code;
            this.Description = product.Description;
            this.Quantity = product.Quantity;
            this.UnitPrice = product.UnitPrice;
            this.SlugUrl = product.SlugUrl;
            this.CategoryName = product.Category.Name;
            this.VendorName = product.Vendor.Name;
            this.VendorId = product.VendorId;
            this.CategoryId = product.CategoryId;
            return this;
        }
    }
}