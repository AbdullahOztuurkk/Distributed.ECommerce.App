namespace CommerceService.Domain.Dtos.Product;

public class ProductResponseDto
{
    public long Id { get; set; }
    public string? Name { get; set; }
    public string? Code { get; set; }
    public string? Description { get; set; }
    public int Quantity { get; set; }
    public int UnitPrice { get; set; }
    public string? SlugUrl { get; set; }
    public long CategoryId { get; set; }
    public long VendorId { get; set; }
    public string? CategoryName { get; set; }
    public string? VendorName { get; set; }

    public ProductResponseDto Map(Concrete.Product product)
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